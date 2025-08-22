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
}
