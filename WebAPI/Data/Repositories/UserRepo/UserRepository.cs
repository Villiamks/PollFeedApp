using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using WebAPI.Interfaces;

namespace WebAPI.Data.UserRepo;

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
            .Include(v => v.Votes)
            .ToListAsync();
    }

    public async Task<Users?> GetByIdAsync(int id)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Set<Users>()
            .Include(p => p.Polls)
            .Include(v => v.Votes)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Users> CreateAsync(Users entity)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Set<Users>().Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<Users?> DeleteAsync(int id)
    {
        using var context = _contextFactory.CreateDbContext();
        var entity = await context.Set<Users>()
            .FirstOrDefaultAsync(u => u.Id == id);

        if (entity == null) return null;
        
        context.Set<Users>().Remove(entity);
        await context.SaveChangesAsync();
        return entity;
    }
}