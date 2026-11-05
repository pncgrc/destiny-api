using DestinyAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DestinyAPI.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DestinyDbContext>
    {
        public DestinyDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DestinyDbContext>();

            var connectionString = "server=localhost;port=3306;database=DestinyDb;user=root;password=root";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new DestinyDbContext(optionsBuilder.Options);
        }
    }
}
