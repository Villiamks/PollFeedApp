using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using WebAPI.Interfaces;

namespace WebAPI.Data.VoteRepo;

public class VoteRepository : IRepository<Votes>
{
    private IDbContextFactory<ApplicationDbContext>  _contextFactory;
    
    public async Task<IEnumerable<Votes>> GetAllAsync()
    {
        using var context = _contextFactory.CreateDbContext();
        return await  context.Set<Votes>()
            .Include(u => u.User)
            .Include(vo => vo.VoteOption)
            .ToListAsync();
    }

    public async Task<Votes?> GetByIdAsync(int id)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Set<Votes>()
            .Include(u => u.User)
            .Include(vo => vo.VoteOption)
            .FirstOrDefaultAsync(v => v.VoteId == id);
    }

    public async Task<Votes> CreateAsync(Votes entity)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Set<Votes>().Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<Votes?> DeleteAsync(int id)
    {
        using var context = _contextFactory.CreateDbContext();
        var entity = await context.Set<Votes>()
            .FirstOrDefaultAsync(v => v.VoteId == id);
        
        if (entity == null) return  null;

        context.Set<Votes>().Remove(entity);
        await context.SaveChangesAsync();
        return entity;
    }
}