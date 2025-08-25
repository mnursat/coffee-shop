namespace CoffeeShop.Infrastructure;

public static class AuthorizationPolicies
{
    public const string RequireSuperAdmin = "RequireSuperAdmin";
    public const string RequireAdmin = "RequireAdmin";
    public const string RequireUser = "RequireUser";
}

