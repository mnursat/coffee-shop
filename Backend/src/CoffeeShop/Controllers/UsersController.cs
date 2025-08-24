using CoffeeShop.Services;

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
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        await _usersService.RegisterAsync(request.Username, request.Email, request.Password);
        return Ok();
    }

    [HttpPost("login")]
    public async Task Login(AuthenticateUserRequest request)
    {
        var token = await _usersService.AuthenticateAsync(request.Email, request.Password);

        HttpContext.Response.Cookies.Append("tasty-cookies", token);

        await Task.CompletedTask;
    }

    public record RegisterUserRequest(string Username, string Email, string Password);

    public record AuthenticateUserRequest(string Email, string Password);
}