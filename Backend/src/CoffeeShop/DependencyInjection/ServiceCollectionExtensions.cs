using System.Text;

using CoffeeShop.Domain.Enum;
using CoffeeShop.Infrastructure;
using CoffeeShop.Persistence;
using CoffeeShop.Persistence.Repositories;
using CoffeeShop.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
        services.AddScoped<UsersService>();

        return services;
    }

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services
    )
    {
        services.AddSingleton<PasswordHasher>();
        services.AddSingleton<JwtProvider>();

        return services;
    }

    public static IServiceCollection AddConfigurationOptions(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

        return services;
    }

    public static IServiceCollection AddPersistence(
        this IServiceCollection services
    )
    {
        services.AddScoped<CoffeesRepository>();
        services.AddScoped<UsersRepository>();

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

    public static IServiceCollection AddAuthenticationAndAuthorization(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtOptions:SecretKey"]))
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireUser", policy =>
                policy.RequireRole(nameof(Roles.User), nameof(Roles.Admin), nameof(Roles.SuperAdmin)));

            options.AddPolicy("RequireAdmin", policy =>
                policy.RequireRole(nameof(Roles.Admin), nameof(Roles.SuperAdmin)));

            options.AddPolicy("RequireSuperAdmin", policy =>
                policy.RequireRole(nameof(Roles.SuperAdmin)));
        });

        return services;
    }
}
