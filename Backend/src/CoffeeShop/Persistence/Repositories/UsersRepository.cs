using CoffeeShop.Domain;

using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Persistence.Repositories;

public class UsersRepository
{
    private readonly CoffeeDbContext _dbContext;

    public UsersRepository(CoffeeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(User user)
    {
        var userEntity = User.Create(user.Id, user.Username, user.Email, user.PasswordHash);

        await _dbContext.Users.AddAsync(userEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }
}
