using Chickencoop.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Chickencoop.DataAccess
{
    public class ChickencoopContext : DbContext
    {
        public ChickencoopContext(DbContextOptions<ChickencoopContext> options) : base(options)
        {
        }

        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<PersonalLeaderboard> PersonalLeaderboards { get; set; }
        public virtual DbSet<Lobby> Lobbies{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = BookingApp; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PersonalLeaderboard>()
                .HasOne(pl => pl.Player)
                .WithMany(p => p.PersonalLeaderboards)
                .HasForeignKey(pl => pl.PlayerId);

            modelBuilder.Entity<Lobby>()
                .HasOne(e => e.Player)
                .WithMany(p => p.Lobbies)
                .HasForeignKey(e => e.PlayerOneId)
                .HasForeignKey(e => e.PlayerTwoId);
        }
    }
}
