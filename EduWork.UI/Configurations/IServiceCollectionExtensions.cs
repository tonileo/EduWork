using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using Blazored.LocalStorage;
using Common.Contracts;
using Common.Services;
using EduWork.UI.States;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace EduWork.UI.Configurations
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddCascadingAuthenticationState();
            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7104/") });

            services.AddScoped<IAccount, AccountService>();

            services.AddAuthorizationCore();

            services.AddBlazoredLocalStorage();

            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            return services;
        }
    }
}
