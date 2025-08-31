using CoffeeShop.DependencyInjection;
using CoffeeShop.RequestPipeline;

using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddOpenApi()
        .AddCorsPolicy()
        .AddServices()
        .AddInfrastructure()
        .AddConfigurationOptions(builder.Configuration)
        .AddPersistence()
        .AddAuthenticationAndAuthorization(builder.Configuration)
        .AddGlobalErrorHandling()
        .AddDb(builder.Configuration)
        .AddControllers();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.MapScalarApiReference();
        app.MapOpenApi();
    }

    app.UseCors();
    app.ApplyMigrations();
    app.UseGlobalErrorHandling();
    app.MapControllers();
    app.SetCookiePolicy();
    app.UseAuthentication();
    app.UseAuthorization();

    app.Run();
}

