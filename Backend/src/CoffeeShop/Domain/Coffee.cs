namespace CoffeeShop.Domain;

public class Coffee
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public string ImageUrl { get; init; }
    public string CoffeeType { get; set; }
}
