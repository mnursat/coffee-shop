using CoffeeShop.Domain;

using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Persistence.Repositories;

public class CoffeesRepository
{
    private readonly CoffeeDbContext _dbContext;

    public CoffeesRepository(CoffeeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(Coffee coffee)
    {
        await _dbContext.Coffees.AddAsync(coffee);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Coffee?> GetByIdAsync(Guid coffeeId)
    {
        return await _dbContext.Coffees.FindAsync(coffeeId);
    }

    public async Task<bool> ExistsAsync(Guid coffeeId)
    {
        return await _dbContext.Coffees.AnyAsync(c => c.Id == coffeeId);
    }
}