using CoffeeShop.Domain;
using CoffeeShop.Enums;
using CoffeeShop.Infrastructure;
using CoffeeShop.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers;

[ApiController]
[Route("[controller]")]
public class CoffeesController : ControllerBase
{
    private readonly CoffeesService _coffeesService;

    public CoffeesController(CoffeesService coffeesService)
    {
        _coffeesService = coffeesService;
    }

    [Authorize(Policy = AuthorizationPolicies.RequireAdmin)]
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateCoffeeRequest request)
    {
        var coffee = request.ToDomain();

        await _coffeesService.CreateAsync(coffee);

        return CreatedAtAction(
            actionName: nameof(Get),
            routeValues: new { coffeeId = coffee.Id },
            value: CoffeeResponse.FromDomain(coffee)
        );
    }

    [HttpGet("{coffeeId:guid}")]
    public async Task<IActionResult> Get(Guid coffeeId)
    {
        var coffee = await _coffeesService.GetAsync(coffeeId);

        return coffee is null
        ? Problem(statusCode: StatusCodes.Status404NotFound, detail: "Coffee not found")
        : Ok(CoffeeResponse.FromDomain(coffee));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var coffees = await _coffeesService.GetAllAsync();

        return Ok(coffees.Select(CoffeeResponse.FromDomain));
    }

    public record CreateCoffeeRequest(
        string Name,
        string Description,
        decimal Price,
        string ImageUrl,
        CoffeeType CoffeeType
    )
    {
        public Coffee ToDomain()
        {
            return new Coffee()
            {
                Name = Name,
                Description = Description,
                Price = Price,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ImageUrl = ImageUrl,
                CoffeeType = CoffeeType
            };
        }
    }

    public record CoffeeResponse(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        string ImageUrl,
        CoffeeType CoffeeType
    )
    {
        public static CoffeeResponse FromDomain(Coffee coffee)
        {
            return new CoffeeResponse(
                Id: coffee.Id,
                Name: coffee.Name,
                Description: coffee.Description,
                Price: coffee.Price,
                ImageUrl: coffee.ImageUrl,
                CoffeeType: coffee.CoffeeType
            );
        }
    }
}