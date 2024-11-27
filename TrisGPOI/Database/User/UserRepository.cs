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
            return await _context.Users.AnyAsync(x => x.Email == emailOrUsername) ||
                await _context.Users.AnyAsync(x => x.Username == emailOrUsername);
        }
        public async Task<DBUser> FirstOrDefaultUser(string emailOrUsername)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            DBUser? ris = await _context.Users.FirstOrDefaultAsync(x => x.Email == emailOrUsername);
            if (ris == null)
            {
                ris = await _context.Users.FirstOrDefaultAsync(x => x.Username == emailOrUsername);
            }
            if (ris == null)
            {
                ris = new DBUser();
            }
            return ris;
        }
        public async Task AddNewUser(UserRegister model)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            DBUser newUser = new DBUser
            {
                Email = model.Email,
                Username = model.Username,
                Password = model.Password,
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
        }
    }
}
