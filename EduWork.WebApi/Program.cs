using EduWork.WebApi.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

app.Configure(app.Environment, builder.Configuration)
    .MigrateDatabase()
    .SeedData()
    .Run();