using CoffeeShop.Domain;
using CoffeeShop.Errors;
using CoffeeShop.Persistence;
using CoffeeShop.Persistence.Repositories;

namespace CoffeeShop.Services;

public class CoffeesService
{
    private readonly CoffeesRepository _coffeesRepository;

    public CoffeesService(CoffeesRepository coffeesRepository)
    {
        _coffeesRepository = coffeesRepository;
    }

    public async Task CreateAsync(Coffee coffee)
    {
        await _coffeesRepository.CreateAsync(coffee);
    }

    public async Task<Coffee?> GetAsync(Guid coffeeId)
    {
        if (!await _coffeesRepository.ExistsAsync(coffeeId))
        {
            throw new NotFoundException($"Coffee with ID {coffeeId} not found.");
        }

        return await _coffeesRepository.GetByIdAsync(coffeeId);
    }
}