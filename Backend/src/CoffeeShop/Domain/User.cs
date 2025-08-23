namespace CoffeeShop.Domain;

public class User
{
    public Guid Id { get; init; }
    public string Username { get; init; }
    public string Email { get; init; }
    public string PasswordHash { get; init; }

    private User(Guid id, string username, string email, string passwordHash)
    {
        Id = id;
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
    }

    public static User Create(Guid id, string username, string email, string password)
    {
        return new User(id, username, email, password);
    }
}