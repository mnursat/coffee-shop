using CoffeeShop.DependencyInjection;
using CoffeeShop.RequestPipeline;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddGlobalErrorHandling()
        .AddControllers();
}

var app = builder.Build();
{
    app.UseGlobalErrorHandling();
    app.MapControllers();

    app.MapGet("/health", () => "It works!");
    app.Run();
}