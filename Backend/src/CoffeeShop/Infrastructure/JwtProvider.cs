using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using CoffeeShop.Domain;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CoffeeShop.Infrastructure;

public class JwtProvider
{
    private readonly IOptions<JwtOptions> _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options;
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