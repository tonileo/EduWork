using EduWork.DataAccessLayer;
using EduWork.DataAccessLayer.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace EduWork.WebApi.Configurations
{
    public static class WebAppExtensions
    {
        public static WebApplication Configure(this WebApplication app, IWebHostEnvironment enviroment, IConfiguration configuration)
        {
            if (enviroment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseCors(policy =>
                {
                    policy.WithOrigins("https://localhost:7041")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithHeaders(HeaderNames.ContentType);
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            return app;
        }

        public static WebApplication MigrateDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            context.Database.Migrate();

            return app;
        }

        public static WebApplication SeedData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var seedData = scope.ServiceProvider.GetRequiredService<SeedData>();

            seedData.Run();

            return app;
        }
    }
}
