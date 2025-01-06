using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TrisGPOI.Controllers.Tris.Entities;
using TrisGPOI.Core.Game.Exceptions;
using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Hubs.TrisGameHub;

namespace TrisGPOI.Controllers.Tris.Controllers
{
    [ApiController]
    [Route("Tris")]
    public class TrisNormaleController : ControllerBase
    {
        private readonly IGameManager _gameManager;
        public TrisNormaleController(IGameManager gameManager)
        {
            _gameManager = gameManager;
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
                return Ok(new 
                {
                    game = game
                });
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }
    }
}
