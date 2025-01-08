using Microsoft.EntityFrameworkCore;
using TrisGPOI.Core.Reward.Interfaces;
using TrisGPOI.Database.Context;
using TrisGPOI.Database.User.Entities;

namespace TrisGPOI.Database.User
{
    public class UserRewardRepository : IUserRewardRepository
    {
        private readonly IDbContextFactory _dbContextFactory;
        public UserRewardRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<int> GetRewardRemain(string email)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            DBUser? User = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return User.RewardRemain;
        }
        public async Task ResetRewardRemain(string email)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            DBUser? User = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            User.RewardRemain = 10;
            _context.Users.Update(User);
            await _context.SaveChangesAsync();
        }
        public async Task SubtractRewardRemain(string email)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            DBUser? User = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            User.RewardRemain--;
            _context.Users.Update(User);
            await _context.SaveChangesAsync();
        }
    }
}
