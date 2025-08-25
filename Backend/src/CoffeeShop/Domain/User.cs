using CoffeeShop.Domain.Enum;

namespace CoffeeShop.Domain;

public class User
{
    public Guid Id { get; init; }
    public string Username { get; init; }
    public string Email { get; init; }
    public string PasswordHash { get; init; }
    public IEnumerable<Roles> Roles { get; init; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    private User(Guid id, string username, string email, string passwordHash, IEnumerable<Roles> roles)
    {
        Id = id;
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        Roles = roles;
    }

    public static User Create(Guid id, string username, string email, string password, IEnumerable<Roles> roles)
    {
        return new User(id, username, email, password, roles);
    }
}