using Microsoft.EntityFrameworkCore;
using TrisGPOI.Core.User.Entities;
using TrisGPOI.Core.User.Interfaces;
using TrisGPOI.Database.Context;
using TrisGPOI.Database.User.Entities;

namespace TrisGPOI.Database.User
{
    public class UserVittorieRepository : IUserVittorieRepository
    {
        private readonly IDbContextFactory _dbContextFactory;
        public UserVittorieRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<DBUserVittoriePVP> FindVittorieWithEmail(string email)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            var statistiche = await _context.UserVittoriePVP.FirstOrDefaultAsync(x => x.Email == email);
            if (statistiche == null)
            {
                statistiche = await AddNewVittorie(email);
            }
            return statistiche;
        }

        public async Task<DBUserVittoriePVP> AddNewVittorie(string email)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            DBUserVittoriePVP newVittorie = new DBUserVittoriePVP
            {
                Email = email,
            };
            _context.UserVittoriePVP.Add(newVittorie);
            await _context.SaveChangesAsync();
            return newVittorie;
        }

        public async Task UserVictory(string email, string gameType)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            var UserVittorie = await _context.UserVittoriePVP.FirstOrDefaultAsync(x => x.Email == email);
            if (UserVittorie == null)
            {
                await AddNewVittorie(email);
                UserVittorie = await _context.UserVittoriePVP.FirstOrDefaultAsync(x => x.Email == email);
            }
            
            if (gameType == "Normal")
            {
                UserVittorie.VictoryNormal++;
                UserVittorie.GameNormal++;
            }
            else if (gameType == "Infinity")
            {
                UserVittorie.VictoryInfinity++;
                UserVittorie.GameInfinity++;
            }
            else if (gameType == "Ultimate")
            {
                UserVittorie.VictoryUltimate++;
                UserVittorie.GameUltimate++;
            }
            else
            {
                return;
            }

            _context.UserVittoriePVP.Update(UserVittorie);
            await _context.SaveChangesAsync();
        }

        public async Task UserLose(string email, string gameType)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            var UserVittorie = await _context.UserVittoriePVP.FirstOrDefaultAsync(x => x.Email == email);
            if (UserVittorie == null)
            {
                await AddNewVittorie(email);
                UserVittorie = await _context.UserVittoriePVP.FirstOrDefaultAsync(x => x.Email == email);
            }

            if (gameType == "Normal")
            {
                UserVittorie.LossesNormal++;
                UserVittorie.GameNormal++;
            }
            else if (gameType == "Infinity")
            {
                UserVittorie.LossesInfinity++;
                UserVittorie.GameInfinity++;
            }
            else if (gameType == "Ultimate")
            {
                UserVittorie.LossesUltimate++;
                UserVittorie.GameUltimate++;
            }
            else
            {
                return;
            }

            _context.UserVittoriePVP.Update(UserVittorie);
            await _context.SaveChangesAsync();
        }

        public async Task UserDraw(string email, string gameType)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            var UserVittorie = await _context.UserVittoriePVP.FirstOrDefaultAsync(x => x.Email == email);
            if (UserVittorie == null)
            {
                await AddNewVittorie(email);
                UserVittorie = await _context.UserVittoriePVP.FirstOrDefaultAsync(x => x.Email == email);
            }

            if (gameType == "Normal")
            {
                UserVittorie.GameNormal++;
            }
            else if (gameType == "Infinity")
            {
                UserVittorie.GameInfinity++;
            }
            else if (gameType == "Ultimate")
            {
                UserVittorie.GameUltimate++;
            }
            else
            {
                return;
            }

            _context.UserVittoriePVP.Update(UserVittorie);
            await _context.SaveChangesAsync();
        }
    }
}
