using Microsoft.EntityFrameworkCore;
using TrisGPOI.Core.Money.Interfaces;
using TrisGPOI.Database.Context;
using TrisGPOI.Database.User.Entities;

namespace TrisGPOI.Database.User
{
    public class UserMoneyXORepository : IUserMoneyXORepository
    {
        private readonly IDbContextFactory _dbContextFactory;
        public UserMoneyXORepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<int> GetMoney(string email)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            DBUser? user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user.MoneyXO;
        }
        public async Task SetMoney(string email, int money)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            user.MoneyXO = money;
            await _context.SaveChangesAsync();
        }
        public async Task AddMoney(string email, int money)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            user.MoneyXO += money;
            await _context.SaveChangesAsync();
        }
        public async Task RemoveMoney(string email, int money)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            user.MoneyXO -= money;
            await _context.SaveChangesAsync();
        }
    }
}
