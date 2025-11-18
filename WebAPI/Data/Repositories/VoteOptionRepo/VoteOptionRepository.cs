using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using WebAPI.Interfaces;

namespace WebAPI.Data.VoteOptionRepo;

public class VoteOptionRepository : IRepository<VoteOptions>
{
    private IDbContextFactory<ApplicationDbContext> _contextFactory;

    public VoteOptionRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<VoteOptions>> GetAllAsync()
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Set<VoteOptions>()
            .Include(p => p.Poll)
            .Include(v => v.Votes)
            .ToListAsync();
    }

    public async Task<VoteOptions?> GetByIdAsync(int id)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Set<VoteOptions>()
            .Include(p => p.Poll)
            .Include(v => v.Votes)
            .FirstOrDefaultAsync(vo => vo.VoteOptionId == id);
    }

    public async Task<VoteOptions> CreateAsync(VoteOptions entity)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Set<VoteOptions>().Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<VoteOptions?> DeleteAsync(int id)
    {
        using var context = _contextFactory.CreateDbContext();
        var entity = await context.Set<VoteOptions>()
            .FirstOrDefaultAsync(vo => vo.VoteOptionId == id);

        if (entity == null) return null;

        context.Set<VoteOptions>().Remove(entity);
        await context.SaveChangesAsync();
        return entity;
    }
}