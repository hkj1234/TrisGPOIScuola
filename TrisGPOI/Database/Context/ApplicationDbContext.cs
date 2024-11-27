using Microsoft.EntityFrameworkCore;
using TrisGPOI.Database.User.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<DBUser> Customers { get; set; }
}