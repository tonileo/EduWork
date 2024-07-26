using System.Reflection.Metadata.Ecma335;
using EduWork.UI.Authentication;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace EduWork.UI.Configurations
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, WebAssemblyHostConfiguration configuration)
        {
            var azureAdOptions = new ClientAzureAdOptions();
            var downstreamApiOptions = new DownstreamApiOptions();

            configuration.GetSection(ClientAzureAdOptions.Section).Bind(azureAdOptions);
            configuration.GetSection(DownstreamApiOptions.Section).Bind(downstreamApiOptions);

            //services.AddScoped<ChartJs>();

            services.AddScoped(sp =>
            {
                var authorizationMessageHandler = sp.GetRequiredService<AuthorizationMessageHandler>();
                authorizationMessageHandler.InnerHandler = new HttpClientHandler();

                authorizationMessageHandler = authorizationMessageHandler.ConfigureHandler(
                    authorizedUrls: new[] { downstreamApiOptions.BaseUrl },
                    scopes: new[] { downstreamApiOptions.Scope });

                return new HttpClient(authorizationMessageHandler)
                {
                    BaseAddress = new Uri(downstreamApiOptions.BaseUrl ?? string.Empty)
                };
            });

            services.AddMsalAuthentication<RemoteAuthenticationState, UserAccount>(options =>
            {
                configuration.Bind(ClientAzureAdOptions.Section, options.ProviderOptions.Authentication);
                //options.ProviderOptions.LoginMode = "redirect";
                options.ProviderOptions.LoginMode = "popup";
                options.ProviderOptions.DefaultAccessTokenScopes.Add(downstreamApiOptions.Scope);
                options.ProviderOptions.Cache.CacheLocation = "localStorage";
            }).AddAccountClaimsPrincipalFactory<RemoteAuthenticationState, UserAccount, UserAccountFactory>();

            return services;
        }
    }
}
