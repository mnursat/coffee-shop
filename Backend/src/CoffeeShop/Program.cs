using CoffeeShop.DependencyInjection;
using CoffeeShop.RequestPipeline;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
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
    app.ApplyMigrations();
    app.UseGlobalErrorHandling();
    app.MapControllers();
    app.SetCookiePolicy();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapGet("/health", () => "It works!");
    app.Run();
}