using CoffeeShop.Persistence;
using CoffeeShop.Persistence.Repositories;
using CoffeeShop.Services;

using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGlobalErrorHandling(
        this IServiceCollection services
    )
    {
        services.AddProblemDetails();

        return services;
    }

    public static IServiceCollection AddServices(
        this IServiceCollection services
    )
    {
        services.AddScoped<CoffeesService>();

        return services;
    }

    public static IServiceCollection AddPersistence(
        this IServiceCollection services
    )
    {
        services.AddScoped<CoffeesRepository>();

        return services;
    }

    public static IServiceCollection AddDb(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<CoffeeDbContext>(options =>
            options.UseNpgsql(configuration["Database:ConnectionStrings:DefaultConnection"]));

        return services;
    }
}
