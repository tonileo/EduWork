using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using Blazored.LocalStorage;
using Common.Contracts;
using Common.Services;
using EduWork.UI.States;
using Microsoft.AspNetCore.Components.Authorization;

namespace EduWork.UI.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var deafultApiOptions = new DefaultApiOptions();

            configuration.GetSection(DefaultApiOptions.Section).Bind(deafultApiOptions);

            services.AddCascadingAuthenticationState();
            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(deafultApiOptions.BaseUrl) });

            services.AddScoped<IAccount, AccountService>();

            services.AddAuthorizationCore();

            services.AddBlazoredLocalStorage();

            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            return services;
        }
    }
}
