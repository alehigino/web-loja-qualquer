using LojaQualquer.Web.Application;
using LojaQualquer.Web.Application.Interfaces;
using LojaQualquer.Web.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LojaQualquer.Web.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void ResolveDependencies(this IServiceCollection services)
        {
            services.AddHttpClient<ILoginApplication, LoginApplication>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUser, User>();
        }
    }
}