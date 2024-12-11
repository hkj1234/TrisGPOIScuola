using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace TrisGPOI.Database.Context
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly IConfiguration _configuration;
        public DbContextFactory(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public ApplicationDbContext CreateMySQLDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            optionsBuilder
                .UseMySql(connectionString, 
                new MySqlServerVersion(new Version(5, 5, 62)));

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            return dbContext;
        }
    }
}
