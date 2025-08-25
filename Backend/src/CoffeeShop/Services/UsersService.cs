using CoffeeShop.Domain;
using CoffeeShop.Domain.Enum;
using CoffeeShop.Errors;
using CoffeeShop.Infrastructure;
using CoffeeShop.Persistence.Repositories;

namespace CoffeeShop.Services;

public class UsersService
{
    private readonly UsersRepository _usersRepository;
    private readonly PasswordHasher _passwordHasher;
    private readonly JwtProvider _jwtProvider;

    public UsersService(
        UsersRepository usersRepository,
        PasswordHasher passwordHasher,
        JwtProvider jwtProvider
    )
    {
        _usersRepository = usersRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task RegisterAsync(string username, string email, string password)
    {
        var existingUser = await _usersRepository.GetByEmailAsync(email);
        if (existingUser != null)
        {
            throw new AlreadyExistsException("User with this email already exists.");
        }

        var passwordHash = _passwordHasher.Generate(password);
        var user = User.Create(Guid.NewGuid(), username, email, passwordHash,
            [Roles.User]);

        await _usersRepository.AddAsync(user);
    }

    public async Task<string> AuthenticateAsync(string email, string password)
    {
        var user = await _usersRepository.GetByEmailAsync(email);
        
        if (user == null || !_passwordHasher.Verify(password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        var token = _jwtProvider.GenerateToken(user);
        return token;
    }

    public async Task EditAsync(Guid userId, string username, string email)
    {
        var user = await _usersRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }

        var updatedUser = User.Create(user.Id, username, email, user.PasswordHash, user.Roles);
        await _usersRepository.UpdateAsync(updatedUser);
    }

    public async Task DeleteAsync(Guid userId)
    {
        var user = await _usersRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }

        await _usersRepository.DeleteAsync(user);
    }

}