using System.Reflection.Metadata.Ecma335;
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

            services.AddMsalAuthentication(options =>
            {
                configuration.Bind(ClientAzureAdOptions.Section, options.ProviderOptions.Authentication);
                options.ProviderOptions.LoginMode = "redirect";
                options.ProviderOptions.DefaultAccessTokenScopes.Add(downstreamApiOptions.Scope);
            });

            return services;
        }
        //public static void AddRootComponents(this WebAssemblyHostBuilder builder)
        //{
        //    builder.RootComponents.Add<App>("#app");
        //    builder.RootComponents.Add<HeadOutlet>("head::after");
        //}
    }
}
