using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace LojaQualquer.Web.Application.Interfaces
{
    public interface IUser
    {
        string Name { get;  }
        string Token { get; }
        bool IsAuthenticated();
    }

    public class User : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public string Name => IsAuthenticated() ? _accessor.HttpContext.User.GetClaim("Name") : "";
        public string Token => IsAuthenticated() ? _accessor.HttpContext.User.GetClaim("JWT") : "";
        public User(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }

    public static class ClaimsPrincipalExtensions
    {
        public static string GetClaim(this ClaimsPrincipal principal, string claim)
        {
            return principal == null ? "" : principal.FindFirst(claim)?.Value;
        }
    }
}