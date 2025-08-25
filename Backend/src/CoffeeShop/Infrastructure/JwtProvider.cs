using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using CoffeeShop.Domain;
using CoffeeShop.Persistence.Repositories;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CoffeeShop.Infrastructure;

public class JwtProvider
{
    private readonly IOptions<JwtOptions> _options;
    private readonly UsersRepository _usersRepository;

    public JwtProvider(IOptions<JwtOptions> options, UsersRepository usersRepository)
    {
        _options = options;
        _usersRepository = usersRepository;
    }

    internal async Task<User?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
    {
        var user = await _usersRepository.GetByIdAsync(userId);

        if (user is null ||
            user.RefreshToken != refreshToken ||
            user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return null;
        }

        return user;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    internal async Task<string> GenerateAndStoreRefreshTokenAsync(User user)
    {
        var refreshToken = GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_options.Value.RefreshTokenExpiresDays);
        await _usersRepository.UpdateAsync(user); // Persist changes to DB
        return refreshToken;
    }


    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Email, user.Email)
        };

        foreach (var role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
        }

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(_options.Value.ExpiresHours)
        );

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenValue;
    }
}