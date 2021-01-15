using Chickencoop.Models.Enums;
using Chickencoop.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq;
using static Chickencoop.Models.Enums.GamesEnum;
using static Chickencoop.Models.Enums.ResultTypeEnum;

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

            //create realtion to Player
            modelBuilder.Entity<PersonalLeaderboard>()
                .HasOne(pl => pl.Player)
                .WithMany(p => p.PersonalLeaderboards)
                .HasForeignKey(pl => pl.PlayerId);

            //Save enums as strings in the db
            modelBuilder.Entity<PersonalLeaderboard>()
                .Property(pl => pl.Result)
                .HasConversion(new EnumToStringConverter<ResultType>());
            
            modelBuilder.Entity<PersonalLeaderboard>()
                .Property(pl => pl.GameName)
                .HasConversion(new EnumToStringConverter<Games>());

            //relation to Player
            modelBuilder.Entity<Lobby>()
                .HasOne(e => e.Player)
                .WithMany(p => p.Lobbies)
                .HasForeignKey(e => e.PlayerOneId)
                .HasForeignKey(e => e.PlayerTwoId);

            modelBuilder.Entity<Lobby>()
                .Property(l => l.GameName)
                .HasConversion(new EnumToStringConverter<Games>());

            //default value for avatar and background images
            modelBuilder.Entity<Player>()
                .Property(p => p.AvatarUrl)
                .HasDefaultValue("https://cdn.vuetifyjs.com/images/profiles/marcus.jpg");

            modelBuilder.Entity<Player>()
                .Property(p => p.BackgroundUrl)
                .HasDefaultValue("https://cdn.vuetifyjs.com/images/cards/server-room.jpg");
        }
    }
}
