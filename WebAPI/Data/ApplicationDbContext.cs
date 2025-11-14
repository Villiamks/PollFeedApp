using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    //TODO: do DbSet for all entities
    
}