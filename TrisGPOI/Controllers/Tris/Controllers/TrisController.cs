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
    }
}
