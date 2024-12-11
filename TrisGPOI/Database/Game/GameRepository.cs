using Microsoft.EntityFrameworkCore;
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
        public async Task StartJoinGame(string typeGame, string emailPlayer1)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            DBGame newGame = new DBGame
            {
                GameType = typeGame,
                Player1 = emailPlayer1,
                Board = "---------",
                CurrentPlayer = emailPlayer1,
                IsFinished = false,
                LastMoveTime = DateTime.UtcNow.AddHours(1),
            };
            _context.Game.Add(newGame);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> JoinSomeGame(string typeGame, string emailPlayer2)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            DBGame? game = await _context.Game.FirstOrDefaultAsync(x => x.GameType == typeGame && x.Player2 == null);
            if (game == null)
            {
                return false;
            }
            game.Player2 = emailPlayer2;
            game.LastMoveTime = DateTime.UtcNow;

            _context.Game.Update(game);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<DBGame?> SearchPlayerPlayingOrWaitingGame(string email)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            return await _context.Game.FirstOrDefaultAsync(x => (x.IsFinished == false && x.LastMoveTime.AddMinutes(1) >= DateTime.UtcNow) && (x.Player1 == email || x.Player2 == email));
        }
        public async Task<DBGame?> SearchPlayerPlayingGame(string email)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            return await _context.Game.FirstOrDefaultAsync(x => (x.IsFinished == false && x.LastMoveTime.AddMinutes(1) >= DateTime.UtcNow) && (x.Player1 == email || x.Player2 == email) && x.Player2 != null);
        }
        public async Task<string> PlayMove(string email, int position)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            var game = await _context.Game.FirstOrDefaultAsync(x => (x.IsFinished == false && x.LastMoveTime.AddMinutes(1) >= DateTime.UtcNow) && (x.Player1 == email || x.Player2 == email) && x.Player2 != null);
            if (game == null)
            {
                return null;
            }
            //aggiornamento board
            var temp = game.Board.ToCharArray();
            temp[position] = game.Player1 == email ? '1' : '2';
            game.Board = new string(temp);

            //next player
            game.CurrentPlayer = game.Player1 == email ? game.Player2 : game.Player1;

            //aggiornamento tempo
            game.LastMoveTime = DateTime.UtcNow;

            _context.Game.Update(game);
            await _context.SaveChangesAsync();
            return game.Board;
        }

        public async Task<string> GameFinished(int id)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            var game = await _context.Game.FirstOrDefaultAsync(x => x.Id == id);
            if (game == null)
            {
                return null;
            }
            game.IsFinished = true;

            _context.Game.Update(game);
            await _context.SaveChangesAsync();
            return game.Board;
        }

        public async Task CancelSearchGame(string email)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            var game = _context.Game.FirstOrDefault(x => x.Player1 == email && x.Player2 == null);
            if (game == null)
            {
                return;
            }
            _context.Game.Remove(game);
            await _context.SaveChangesAsync();
        }
    }
}
