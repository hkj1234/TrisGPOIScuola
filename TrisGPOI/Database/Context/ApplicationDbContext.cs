using Microsoft.EntityFrameworkCore;
using TrisGPOI.Core.ReceiveBox.Entities;
using TrisGPOI.Database.Friend.Entities;
using TrisGPOI.Database.Game.Entities;
using TrisGPOI.Database.OTP.Entities;
using TrisGPOI.Database.User.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<DBUser> Users { get; set; }
    public DbSet<DBUserVittoriePVP> UserVittoriePVP { get; set; }
    public DbSet<DBOtpEntity> OTP { get; set; }
    public DbSet<DBGame> Game { get; set; }
    public DbSet<DBFriend> Friend { get; set; }
    public DbSet<DBFriendRequest> FriendRequest { get; set; }
    public DbSet<DBReceiveBox> ReceiveBox { get; set; }

}