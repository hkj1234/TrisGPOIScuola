using Microsoft.EntityFrameworkCore;
using TrisGPOI.Database.OTP.Entities;
using TrisGPOI.Database.User.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<DBUser> Users { get; set; }
    public DbSet<DBOtpEntity> OTP { get; set; }
}