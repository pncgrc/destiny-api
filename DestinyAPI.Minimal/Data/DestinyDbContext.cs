using DestinyAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DestinyAPI.Data
{
    public class DestinyDbContext : DbContext
    {
        public DestinyDbContext(DbContextOptions<DestinyDbContext> options) : base(options) { }

        public DbSet<Character> Characters => Set<Character>();
        public DbSet<Weapon> Weapons => Set<Weapon>();
        public DbSet<Faction> Factions => Set<Faction>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = "server=localhost;port=3306;database=DestinyDb;user=root;password=root";
                optionsBuilder
                    .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                    .EnableSensitiveDataLogging()
                    .LogTo(message => Debug.WriteLine(message));
            }
        }

    }
}
