namespace CoffeeShop.Persistence;

using CoffeeShop.Domain;
using Microsoft.EntityFrameworkCore;

public class CoffeeDbContext : DbContext
{
    public CoffeeDbContext(DbContextOptions<CoffeeDbContext> options)
        : base(options)
    {
    }

    public DbSet<Coffee> Coffees { get; set; }
    public DbSet<User> Users { get; set; }
}