using Microsoft.EntityFrameworkCore;
using TrisGPOI.Core.Level.Entities;
using TrisGPOI.Core.Level.Interfaces;
using TrisGPOI.Database.Context;

namespace TrisGPOI.Database.User
{
    public class UserLevelRepository : IUserLevelRepository
    {
        private readonly IDbContextFactory _dbContextFactory;
        public UserLevelRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<LevelAndExperience> GetLevelAndExperience(string email)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return new LevelAndExperience { Level = user.Level, Experience = user.Experience };
        }
        public async Task SetLevelAndExperience(string email, LevelAndExperience levelAndExperience)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            user.Level = levelAndExperience.Level;
            user.Experience = levelAndExperience.Experience;
            await _context.SaveChangesAsync();
        }
    }
}
