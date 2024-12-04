using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Database.Context;
using TrisGPOI.Database.Game.Entities;

namespace TrisGPOI.Database.Game
{
    public class GameRepository : IGameRepository
    {
        private readonly IDbContextFactory _dbContextFactory;
        public GameRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task StartSearchGame(string typeGame, string emailPlayer1)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            DBGame newGame = new DBGame
            {
                
            };
            _context.Game.Add(newGame);
            await _context.SaveChangesAsync();
        }
    }
}
