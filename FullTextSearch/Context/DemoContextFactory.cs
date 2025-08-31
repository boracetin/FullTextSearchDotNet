using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FullTextSearch.Context
{
    public class DemoContextFactory : IDesignTimeDbContextFactory<DemoContext>
    {
        public DemoContext CreateDbContext(string[] args)
        {
            // appsettings.json'dan connection string oku
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<DemoContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new DemoContext(optionsBuilder.Options);
        }
    }
}
