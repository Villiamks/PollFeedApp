using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using WebAPI.Interfaces;

namespace WebAPI.Data;

public class UserRepository : IRepository<Users>
{
    private IDbContextFactory<ApplicationDbContext> _contextFactory;

    public UserRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<Users>> GetAllAsync()
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Set<Users>()
            .Include(p => p.Polls)
            .ToListAsync();
    }

    public async Task<Users> GetByIdAsync(int id)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Set<Users>()
            .Include(p => p.Polls)
            .FirstOrDefaultAsync(u => u.UserId == id);
    }
    
    //TODO: Create and Delete
}