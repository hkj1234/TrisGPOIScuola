using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TrisGPOI.Controllers.Tris.Entities;
using TrisGPOI.Core.Game.Exceptions;
using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Core.Home.Interfaces;
using TrisGPOI.Hubs.TrisGameHub;

namespace TrisGPOI.Controllers.Tris.Controllers
{
    [ApiController]
    [Route("Tris")]
    public class TrisController : ControllerBase
    {
        private readonly IGameManager _gameManager;
        private readonly IHomeManager _homeManager;
        internal static Dictionary<int, Timer> gameTimers = new Dictionary<int, Timer>();
        public TrisController(IGameManager gameManager, IHomeManager homeManager)
        {
            _gameManager = gameManager;
            _homeManager = homeManager;
        }

        //PlayingGame
        [Authorize]
        [HttpGet("PlayingGame")]
        public async Task<IActionResult> PlayingGame()
        {
            try
            {
                var email = User?.Identity?.Name;
                var game = await _gameManager.SearchPlayerPlayingGameAsync(email);
                bool Playing = true;
                GameStatus gameStatus = new GameStatus();
                if (game == null)
                {
                    Playing = false;
                    game = await _gameManager.GetLastGame(email);
                }
                if (game != null)
                {
                    
                    gameStatus = new GameStatus
                    {
                        Id = game.Id,
                        GameType = game.GameType,
                        Player1 = game.Player1,
                        Player1Online = await _homeManager.GetUserStatus(game.Player1) != "Offline",
                        Player2 = game.Player2,
                        Player2Online = await _homeManager.GetUserStatus(game.Player2) != "Offline",
                        Board = game.Board,
                        CurrentPlayer = game.CurrentPlayer,
                        LastMoveTime = game.LastMoveTime,
                        IsFinished = game.IsFinished,
                        Winning = game.Winning,
                    };
                }
                return Ok(new 
                {
                    game = gameStatus,
                    Playing = Playing
                });
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }

        [Authorize]
        [HttpGet("Spectator")]
        public async Task<IActionResult> Spectator([FromBody] SpectatorRequest request)
        {
            try
            {
                var email = request.Email;
                var game = await _gameManager.SearchPlayerPlayingGameAsync(email);
                bool Playing = true;
                GameStatus gameStatus = new GameStatus();
                if (game == null)
                {
                    Playing = false;
                    game = await _gameManager.GetLastGame(email);
                }
                if (game != null)
                {
                    
                    gameStatus = new GameStatus
                    {
                        Id = game.Id,
                        GameType = game.GameType,
                        Player1 = game.Player1,
                        Player1Online = await _homeManager.GetUserStatus(game.Player1) != "Offline",
                        Player2 = game.Player2,
                        Player2Online = await _homeManager.GetUserStatus(game.Player2) != "Offline",
                        Board = game.Board,
                        CurrentPlayer = game.CurrentPlayer,
                        LastMoveTime = game.LastMoveTime,
                        IsFinished = game.IsFinished,
                        Winning = game.Winning,
                    };
                }
                return Ok(new 
                {
                    game = gameStatus,
                    Playing = Playing
                });
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }

        [Authorize]
        [HttpPost("PlayOnline")]
        public async Task<IActionResult> PlayOnline([FromBody] PlayOnlineModel request)
        {
            try
            {
                var email = User?.Identity?.Name;
                await _gameManager.JoinGame(email, request.Mode);
                await updateGameTimers(email);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }

        [Authorize]
        [HttpPost("PlayWithCPU")]
        public async Task<IActionResult> PlayWithCPU([FromBody] PlayWithCPUModel request)
        {
            try
            {
                var email = User?.Identity?.Name;
                await _gameManager.PlayWithCPU(email, request.Mode, request.Difficulty);
                await updateGameTimers(email);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }

        [Authorize]
        [HttpPost("AbandonGame")]
        public async Task<IActionResult> AbandonGame()
        {
            try
            {
                var email = User?.Identity?.Name;
                await _gameManager.GameAbandon(email);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }

        [Authorize]
        [HttpPost("PlayMove")]
        public async Task<IActionResult> PlayMove([FromBody] PlayMoveModel request)
        {
            try
            {
                var email = User?.Identity?.Name;
                var board = await _gameManager.PlayMove(email, request.position);
                if (! board.Player2.Contains('@')  && board.Victory == '-')
                {
                    board = await _gameManager.CPUPlayMove(email);
                }
                await updateGameTimers(email);
                return Ok(board);
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }

        private async Task updateGameTimers(string email)
        {
            var game = await _gameManager.GetLastGame(email);
            if (game != null)
            {
                TimeSpan timeRemaining = game.LastMoveTime - DateTime.UtcNow;
                Timer timer = new Timer(TimeOut, game.CurrentPlayer, timeRemaining, Timeout.InfiniteTimeSpan);
                gameTimers[game.Id] = timer;
            }
        }

        private async void TimeOut(object state)
        {
            string player = (string)state;
            await _gameManager.GameAbandon(player);
        }
    }
}
