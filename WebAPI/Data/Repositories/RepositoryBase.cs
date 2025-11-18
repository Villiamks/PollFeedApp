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

    public async Task<T> CreateAsync(T entity)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Set<T>().Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<T?> DeleteAsync(int id)
    {
        using var context = _contextFactory.CreateDbContext();
        var entity = await context.Set<T>()
            .FindAsync(id);

        if (entity == null) return null;

        context.Set<T>().Remove(entity);
        await context.SaveChangesAsync();
        return entity;
    }
}