using Microsoft.EntityFrameworkCore;
using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Database.Context;
using TrisGPOI.Database.Game.Entities;

namespace TrisGPOI.Database.Game
{
    public class GameInviteRepository : IGameInviteRepository
    {
        private readonly IDbContextFactory _dbContextFactory;
        public GameInviteRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<DBGameInvite> GetInvitesByEmail(string email)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            return await _context.GameInvite.Where(invite => invite.InvitedEmail == email).FirstOrDefaultAsync();
        }
        public async Task<bool> AnyInvite(string email)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            return await _context.GameInvite.AnyAsync(invite => invite.InvitedEmail == email || invite.InviterEmail == email);
        }
        public async Task<bool> AnyInviteByInviterEmail(string inviterEmail)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            return await _context.GameInvite.AnyAsync(invite => invite.InviterEmail == inviterEmail);
        }
        public async Task<bool> AnyInviteByInvitedEmail(string invitedEmail)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            return await _context.GameInvite.AnyAsync(invite => invite.InvitedEmail == invitedEmail);
        }
        public async Task InviteGame(string inviterEmail, string invitedEmail, string gameType)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            DBGameInvite newInvite = new DBGameInvite
            {
                InviterEmail = inviterEmail,
                InvitedEmail = invitedEmail,
                GameType = gameType,
                Date = DateTime.UtcNow
            };
            await _context.GameInvite.AddAsync(newInvite);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteInvite(string invitedEmail)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            var invite = await _context.GameInvite.Where(invite => invite.InvitedEmail == invitedEmail).ToListAsync();
            if (invite != null)
            {
                _context.GameInvite.RemoveRange(invite);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<int> GetIdByInvitedEmail(string invitedEmail)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            return await _context.GameInvite.Where(invite => invite.InvitedEmail == invitedEmail).Select(invite => invite.Id).FirstOrDefaultAsync();
        }
        public async Task<int> GetIdByInviterEmail(string inviterEmail)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            return await _context.GameInvite.Where(invite => invite.InviterEmail == inviterEmail).Select(invite => invite.Id).FirstOrDefaultAsync();
        }
    }
}
