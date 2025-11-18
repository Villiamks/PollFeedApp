using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using WebAPI.Interfaces;

namespace WebAPI.Data.VoteOptionRepo;

public class VoteOptionRepository : IRepository<VoteOptions>
{
    private IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public VoteOptionRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IEnumerable<VoteOptions>> GetAllAsync()
    {
        using var context = _dbContextFactory.CreateDbContext();
        return await context.Set<VoteOptions>()
            .Include(p => p.Poll)
            .Include(v => v.Votes)
            .ToListAsync();
    }

    public async Task<VoteOptions?> GetByIdAsync(int id)
    {
        using var context = _dbContextFactory.CreateDbContext();
        return await context.Set<VoteOptions>()
            .Include(p => p.Poll)
            .Include(v => v.Votes)
            .FirstOrDefaultAsync(vo => vo.VoteOptionId == id);
    }
}