using CoffeeShop.Enums;

namespace CoffeeShop.Domain;

public class Coffee
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public string ImageUrl { get; init; }
    public CoffeeType CoffeeType { get; set; }
}
