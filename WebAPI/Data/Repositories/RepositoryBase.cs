using Microsoft.EntityFrameworkCore;
using WebAPI.Interfaces;

namespace WebAPI.Data;

public class RepositoryBase<T> : IRepository<T> where T : class
{
    private IDbContextFactory<ApplicationDbContext> _contextFactory;

    public RepositoryBase(IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Set<T>().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.Set<T>().FindAsync(id);
    }
    
    //TODO: Create and Deletes
}