using Microsoft.EntityFrameworkCore;
using TrisGPOI.Core.User.Entities;
using TrisGPOI.Core.User.Interfaces;
using TrisGPOI.Database.Context;
using TrisGPOI.Database.User.Entities;

namespace TrisGPOI.Database.User
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContextFactory _dbContextFactory;
        public UserRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<bool> ExistUser(string emailOrUsername)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            return await _context.Users.AnyAsync(x => x.Email == emailOrUsername && x.IsActive == true) ||
                await _context.Users.AnyAsync(x => x.Username == emailOrUsername && x.IsActive == true);
        }
        public async Task<DBUser> FirstOrDefaultUser(string emailOrUsername)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            DBUser? ris = await _context.Users.FirstOrDefaultAsync(x => x.Email == emailOrUsername && x.IsActive == true);
            if (ris == null)
            {
                ris = await _context.Users.FirstOrDefaultAsync(x => x.Username == emailOrUsername && x.IsActive == true);
            }
            if (ris == null)
            {
                ris = new DBUser();
            }
            return ris;
        }
        public async Task AddNewUserAsync(UserRegister model)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            DBUser newUser = new DBUser
            {
                Email = model.Email,
                Username = model.Username,
                Password = model.Password,
                IsActive = false
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
        }
        public async Task SetActiveUser(string email)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            DBUser? User = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (User == null)
            {
                return;
            }
            User.IsActive = true;
            _context.Users.Update(User);
            await _context.SaveChangesAsync();
        }
        public async Task ChangeUserPassword(string email, string password)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            DBUser? User = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (User == null)
            {
                return;
            }
            User.Password = password;
            _context.Users.Update(User);
            await _context.SaveChangesAsync();
        }
    }
}
