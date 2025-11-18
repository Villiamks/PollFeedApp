using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using WebAPI.Interfaces;

namespace WebAPI.Data.PollRepo;

public class PollRepository : IRepository<Polls>
{
    
    private IDbContextFactory<ApplicationDbContext> _contextFactory;

    public PollRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<Polls>> GetAllAsync()
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Set<Polls>()
            .Include(u => u.Creator)
            .Include(o => o.Options)
            .ToListAsync();
    }

    public async Task<Polls?> GetByIdAsync(int id)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Set<Polls>()
            .Include(u => u.Creator)
            .Include(o => o.Options)
            .FirstOrDefaultAsync(p => p.PollId == id);
    }

    public async Task<Polls> CreateAsync(Polls entity)
    {
        using var  context = _contextFactory.CreateDbContext();
        context.Set<Polls>().Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<Polls?> DeleteAsync(int id)
    {
        using var context = _contextFactory.CreateDbContext();
        var entity = await context.Set<Polls>()
            .FirstOrDefaultAsync(p => p.PollId == id);

        if (entity == null) return null;

        context.Set<Polls>().Remove(entity);
        await context.SaveChangesAsync();

        return entity;
    }
}