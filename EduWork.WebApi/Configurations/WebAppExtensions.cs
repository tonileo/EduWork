using EduWork.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace EduWork.WebApi.Configuration
{
    public static class WebAppExtensions
    {
        public static WebApplication Configure(this WebApplication app, IWebHostEnvironment enviroment, IConfiguration configuration)
        {
            if (enviroment.IsDevelopment())
            {
                var swaggerOptions = new SwaggerOptions();
                configuration.GetSection(SwaggerOptions.Section).Bind(swaggerOptions);
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.OAuthClientId(swaggerOptions.ClientId);
                    c.OAuthUsePkce();
                });
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }

        public static WebApplication MigrateDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.Migrate();
            }

            return app;
        }
    }
}
