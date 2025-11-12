using ClassLibrary;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Context
{
    public partial class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) {  }

        public DbSet<Users> Users { get; set; }
        public DbSet<Polls> Polls { get; set; }
        public DbSet<Votes> Votes { get; set; }
        public DbSet<VoteOptions> Options { get; set; }

        public List<Users> getUsers()
        {
            var users  = Users
                .Include(u => u.Polls)
                .Include(u => u.Votes)
                .ToList();
            return users;
        }

        public List<Polls> getPolls()
        {
            var polls = Polls
                .Include(p => p.Creator)
                .Include(p => p.Options)
                .ToList();
            return polls;
        }

        public List<VoteOptions> getVoteOptions()
        {
            var voteOptions = Options
                .Include(o => o.Poll)
                .Include(o => o.Votes)
                .ToList();
            return voteOptions;
        }

        public List<Votes> getVotes()
        {
            var votes = Votes
                .Include(v => v.User)
                .Include(v => v.VoteOption)
                .ToList();
            return votes;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Users>(e =>
            {
                e.ToTable("users");
                e.HasKey(x => x.UserId);
                e.Property(x => x.UserId).ValueGeneratedOnAdd().HasColumnName("userid");
                e.Property(x => x.UserName).HasColumnName("username");
                e.Property(x => x.Email).HasColumnName("email");
                e.Property(x => x.PasswordHash).HasColumnName("passwordhash");
                e.Property(x => x.Salt).HasColumnName("salt");
            });
            modelBuilder.Entity<Polls>(e =>
            {
                e.ToTable("polls");
                e.HasKey(x => x.PollId);
                e.Property(x => x.PollId).ValueGeneratedOnAdd().HasColumnName("pollid");
                e.Property(x => x.Question).HasColumnName("question");
                e.Property(x => x.UserId).HasColumnName("userid");
                e.HasOne(x => x.Creator).WithMany(x => x.Polls)
                    .HasForeignKey(x => x.UserId)
                    .HasConstraintName("fk_po_us")
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<VoteOptions>(e =>
            {
                e.ToTable("voteoptions");
                e.HasKey(x => x.VoteOptionId);
                e.Property(x => x.VoteOptionId).ValueGeneratedOnAdd().HasColumnName("voteoptionid");
                e.Property(x => x.Caption).HasColumnName("caption");
                e.Property(x => x.PollId).HasColumnName("pollid");
                e.HasOne(x => x.Poll).WithMany(x => x.Options)
                    .HasForeignKey(x => x.PollId)
                    .HasConstraintName("fk_vop_po")
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Votes>(e =>
            {
                e.ToTable("votes");
                e.HasKey(x => x.VoteId);
                e.Property(x => x.VoteId).ValueGeneratedOnAdd().HasColumnName("voteid");
                e.Property(x => x.UserId).HasColumnName("userid");
                e.Property(x => x.VoteOptionId).HasColumnName("voteoptionid");
                e.HasOne(x => x.User).WithMany(x => x.Votes)
                    .HasForeignKey(x => x.UserId)
                    .HasConstraintName("fk_vo_us")
                    .OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.VoteOption).WithMany(x => x.Votes)
                    .HasForeignKey(x => x.VoteOptionId)
                    .HasConstraintName("fk_vo_vop")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            onModelCreatingPartial(modelBuilder);
        }
        partial void onModelCreatingPartial(ModelBuilder modelBuilder);
    }
}