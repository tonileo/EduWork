using System.Reflection;
using System.Security.Principal;
using EduWork.BusinessLayer.Services;
using EduWork.DataAccessLayer;
using EduWork.DataAccessLayer.Seed;
using EduWork.WebApi.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;

namespace EduWork.WebApi.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(configuration.GetSection(AzureAdOptions.Section))
                .EnableTokenAcquisitionToCallDownstreamApi()
                .AddMicrosoftGraph(configuration.GetSection("MicrosoftGraph"))
                .AddInMemoryTokenCaches();

            services.AddControllers();

            var swaggerOptions = new SwaggerOptions();
            configuration.GetSection(SwaggerOptions.Section).Bind(swaggerOptions);

            var azureAdOptions = new AzureAdOptions();
            configuration.GetSection(AzureAdOptions.Section).Bind(azureAdOptions);

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger Azure Entra", Version = "v1" });
                    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Description = "Oauth2.0 which uses AuthorizationCode flow",
                        Name = "oauth2.0",
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows
                        {
                            AuthorizationCode = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri(swaggerOptions.AuthorizationUrl),
                                TokenUrl = new Uri(swaggerOptions.TokenUrl),
                                Scopes = new Dictionary<string, string>
                                {
                                    {swaggerOptions.Scope, "Access API as User" }
                                }
                            }
                        }
                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme, Id = "oauth2"}
                            },
                            new[]{ swaggerOptions.Scope }
                        }
                    });
                });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddHttpContextAccessor();
            services.AddTransient<Authentication.IIdentity, Identity>();

            services.AddScoped<UserProfileService>();
            services.AddScoped<UserProjectTimeService>();

            services.AddDbContext<AppDbContext>();

            services.AddScoped<SeedData>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            return services;
        }
    }
}
