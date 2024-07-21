using EduWork.WebApi.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SwaggerOptions>(builder.Configuration.GetSection(SwaggerOptions.Section));

builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

app.Configure(app.Environment, builder.Configuration)
    .MigrateDatabase()
    .SeedData()
    .Run();