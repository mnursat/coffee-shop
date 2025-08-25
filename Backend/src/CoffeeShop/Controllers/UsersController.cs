using CoffeeShop.Contracts.Jwt;
using CoffeeShop.Domain.Enum;
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
    public async Task<ActionResult<TokenResponseDto>> LoginAsync(AuthenticateUserRequest request)
    {
        var result = await _usersService.AuthenticateAsync(request.Email, request.Password);

        if (result is null)
        {
            return BadRequest("Invalid email or password.");
        }

        return Ok(result);
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
        public string Username { get; init; }
        public string Email { get; init; }
    }
}
