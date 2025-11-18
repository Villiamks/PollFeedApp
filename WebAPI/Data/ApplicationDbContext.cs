using ClassLibrary;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Users> Users => Set<Users>();
    public DbSet<Votes> Votes => Set<Votes>();
    public DbSet<VoteOptions> VoteOptions => Set<VoteOptions>();
    public DbSet<Polls> Polls => Set<Polls>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}