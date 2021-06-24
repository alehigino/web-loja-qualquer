using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace LojaQualquer.Web.Extensions
{
    public interface IUser
    {
        string Name { get;  }
        bool IsAuthenticated();
    }

    public class User : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public string Name => IsAuthenticated() ? _accessor.HttpContext.User.GetClaim("Name") : "";
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