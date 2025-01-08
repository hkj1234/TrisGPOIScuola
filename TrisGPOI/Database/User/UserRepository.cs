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
        public async Task<bool> ExistActiveUser(string emailOrUsername)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            return await _context.Users.AnyAsync(x => x.Email == emailOrUsername && x.IsActive == true) ||
                await _context.Users.AnyAsync(x => x.Username == emailOrUsername && x.IsActive == true);
        }
        public async Task<bool> ExistUser(string emailOrUsername)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            return await _context.Users.AnyAsync(x => x.Email == emailOrUsername) ||
                await _context.Users.AnyAsync(x => x.Username == emailOrUsername);
        }
        public async Task<DBUser> FirstOrDefaultActiveUser(string emailOrUsername)
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
                IsActive = false,

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
        public async Task ChangeUserDescription(string email, string description)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            DBUser? User = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (User == null)
            {
                return;
            }
            User.Description = description;
            _context.Users.Update(User);
            await _context.SaveChangesAsync();
        }
        public async Task ChangeUserFoto(string email, string StringaFoto)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            DBUser? User = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (User == null)
            {
                return;
            }
            User.FotoProfilo = StringaFoto;
            _context.Users.Update(User);
            await _context.SaveChangesAsync();
        }
        public async Task ChangeUserStatus(string email, string Status)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            DBUser? User = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (User == null)
            {
                return;
            }
            User.Status = Status;
            _context.Users.Update(User);
            await _context.SaveChangesAsync();
        }
        public async Task<string> GetEmailByUsername(string username)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            return (await _context.Users.FirstOrDefaultAsync(x => x.Username == username)).Email;
        }
        public async Task<int> GetUserStatusNumber(string email)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            return (await _context.Users.FirstOrDefaultAsync(x => x.Email == email)).StatusNumber;
        }
        public async Task AddUserStatusNumber(string email)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            DBUser? User = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (User == null)
            {
                return;
            }
            User.StatusNumber++;
            _context.Users.Update(User);
            await _context.SaveChangesAsync();
        }
        public async Task SubUserStatusNumber(string email)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            DBUser? User = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (User == null)
            {
                return;
            }
            User.StatusNumber--;
            _context.Users.Update(User);
            await _context.SaveChangesAsync();
        }
        public async Task ResetUserStatusNumber(string email)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            DBUser? User = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (User == null)
            {
                return;
            }
            User.StatusNumber = 0;
            _context.Users.Update(User);
            await _context.SaveChangesAsync();
        }
    }
}
