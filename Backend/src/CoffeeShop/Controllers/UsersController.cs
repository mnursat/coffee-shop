using CoffeeShop.Contracts.Jwt;
using CoffeeShop.Infrastructure;
using CoffeeShop.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly UsersService _usersService;

    public UsersController(UsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterUserRequest request)
    {
        await _usersService.RegisterAsync(request.Username, request.Email, request.Password);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(AuthenticateUserRequest request)
    {
        var result = await _usersService.AuthenticateAsync(request.Email, request.Password);

        if (result is null)
        {
            return BadRequest("Invalid email or password.");
        }

        // Устанавливаем JWT как httpOnly cookie
        Response.Cookies.Append("jwt", result.AccessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = false, // Только для HTTPS
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddHours(2)
        });

        // Можно вернуть только статус успеха, не возвращая сам токен
        return Ok(new { message = "Login successful" });
    }

    [Authorize(Policy = AuthorizationPolicies.RequireUser)]
    [HttpGet("check-auth")]
    public IActionResult CheckAuth()
    {
        // Если пользователь авторизован, просто возвращаем 200 OK
        return Ok(new { authenticated = true });
    }

    [Authorize(Policy = AuthorizationPolicies.RequireUser)]
    [HttpPost("refresh-token")]
    public async Task<ActionResult<TokenResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request)
    {
        var result = await _usersService.RefreshTokenAsync(request);

        if (result is null)
        {
            return Unauthorized("Invalid refresh token.");
        }

        return Ok(result);
    }

    [Authorize(Policy = AuthorizationPolicies.RequireUser)]
    [HttpPost("logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        await _usersService.LogoutAsync(userId);

        // Явно удаляем JWT куку
        Response.Cookies.Append("jwt", "", new CookieOptions
        {
            Path = "/",
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(-1)
        });

        return NoContent();
    }

    [Authorize(Policy = AuthorizationPolicies.RequireUser)]
    [HttpPost("edit")]
    public async Task<IActionResult> Edit(EditUserRequest request)
    {
        await _usersService.EditAsync(request.UserId, request.Username, request.Email);
        return Ok();
    }

    [Authorize(Policy = AuthorizationPolicies.RequireSuperAdmin)]
    [HttpDelete("delete/{userId:guid}")]
    public async Task<IActionResult> Delete(Guid userId)
    {
        await _usersService.DeleteAsync(userId);
        return NoContent();
    }

    public record RegisterUserRequest(
        string Username,
        string Email,
        string Password);

    public record AuthenticateUserRequest(string Email, string Password);

    public record EditUserRequest
    {
        public Guid UserId { get; init; }
        public required string Username { get; init; }
        public required string Email { get; init; }
    }
}
