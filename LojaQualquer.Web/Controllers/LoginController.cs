using AutoMapper;
using LojaQualquer.Web.Application.Interfaces;
using LojaQualquer.Web.Application.Models.Request;
using LojaQualquer.Web.Application.Models.Response;
using LojaQualquer.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LojaQualquer.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginApplication _loginApplication;
        private readonly IMapper _mapper;
        private readonly IUser _user;

        public LoginController(ILoginApplication loginApplication, IMapper mapper, IUser user)
        {
            _loginApplication = loginApplication;
            _mapper = mapper;
            _user = user;
        }

        public IActionResult Index()
        {
            return _user.IsAuthenticated() 
                ? RedirectToAction("Index", "Product")
                : View();
        }

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var response = await _loginApplication.PostAsync(_mapper.Map<LoginRequest>(model));

            if (response.ResponseError != null)
            {
                ModelState.AddModelError(string.Empty, response.ResponseError.Message);

                return View("Index");
            }

            await HandleLogin(response);

            return RedirectToAction("Index", "Product");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

        private async Task HandleLogin(TokenResponse response)
        {
            var token = new JwtSecurityTokenHandler().ReadToken(response.Token) as JwtSecurityToken;

            var claims = new List<Claim>
            {
                new Claim("JWT", response.Token), 
                new Claim("Name", response.Name)
            };
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}
