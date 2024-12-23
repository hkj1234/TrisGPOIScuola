using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TrisGPOI.Controllers.Tris.Entities;
using TrisGPOI.Core.Game.Exceptions;
using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Hubs.Game;

/*
namespace TrisGPOI.Controllers.Tris.Controllers
{
    [ApiController]
    [Route("Normal")]
    public class TrisNormaleController : ControllerBase
    {
        private readonly IGameManager _gameManager;
        private readonly IHubContext<Hubs.Game.TrisNormaleHub> _hubContext;
        public TrisNormaleController(IGameManager gameManager, IHubContext<Hubs.Game.TrisNormaleHub> hubContext)
        {
            _gameManager = gameManager;
            _hubContext = hubContext;
        }


        //SerchGame
        [Authorize]
        [HttpPost("SearchGame")]
        public async Task<IActionResult> SearchGame()
        {
            try
            {
                var email = HttpContext.User.Identity.Name;
                await _gameManager.JoinGame(email, "Normal");
                return Ok();
            }
            catch (ExistGameException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }


        //CancelSearchGame
        [Authorize]
        [HttpDelete("CancelSearchGame")]
        public async Task<IActionResult> CancelSearchGame()
        {
            try
            {
                var email = HttpContext.User.Identity.Name;
                await _gameManager.CancelSearchGame(email);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }


        //PlayMove
        [Authorize]
        [HttpPost("PlayMove")]
        public async Task<IActionResult> PlayMove([FromBody] PlayMoveModel request)
        {
            try
            {
                var email = HttpContext.User.Identity.Name;
                var board = await _gameManager.PlayMove(email, request.position);

                //comunicazione con player avversario
                var game = await _gameManager.SearchPlayerPlayingOrWaitingGameAsync(email);
                string groupName = game.Id.ToString();
                await _hubContext.Clients.Group(groupName).SendAsync("ReceiveMove", board); 

                //se utente due è ai, gioca ai
                if ((!game.Player2.Contains("@")))
                {
                    board = await _gameManager.CPUPlayMove(email);
                    await _hubContext.Clients.Group(groupName).SendAsync("ReceiveMove", board);
                }

                return Ok(board);
            }
            catch (NoGamePlayingException e)
            {
                return Conflict();
            }
            catch (InvalidPlayerMoveException e)
            {
                return BadRequest();
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }
    }
}
*/