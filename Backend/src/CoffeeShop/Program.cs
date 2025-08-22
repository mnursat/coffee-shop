using CoffeeShop.DependencyInjection;
using CoffeeShop.RequestPipeline;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddServices()
        .AddPersistence()
        .AddGlobalErrorHandling()
        .AddDb(builder.Configuration)
        .AddControllers();
}

var app = builder.Build();
{
    app.ApplyMigrations();
    app.UseGlobalErrorHandling();
    app.MapControllers();

    app.MapGet("/health", () => "It works!");
    app.Run();
}