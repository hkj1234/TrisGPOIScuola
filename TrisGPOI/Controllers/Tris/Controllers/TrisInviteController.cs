using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Controllers.Tris.Entities;

namespace TrisGPOI.Controllers.Tris.Controllers
{
    [ApiController]
    [Route("TrisInvite")]
    public class TrisInviteController : Controller
    {
        private readonly IGameInviteManager _gameInviteManager;
        public TrisInviteController(IGameInviteManager gameInviteManager)
        {
            _gameInviteManager = gameInviteManager;
        }


        // GetInvite
        [Authorize]
        [HttpGet("GetInvite")]
        public async Task<IActionResult> GetInvite()
        {
            try
            {
                var invites = await _gameInviteManager.GetInvitesByEmail(User.Identity.Name);
                return Ok(new {
                    Invites = invites
                });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // AcceptInvite
        [Authorize]
        [HttpPost("AcceptInvite")]
        public async Task<IActionResult> AcceptInvite()
        {
            try
            {
                await _gameInviteManager.AcceptInvite(User.Identity.Name);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DeclineInvite
        [Authorize]
        [HttpPost("DeclineInvite")]
        public async Task<IActionResult> DeclineInvite()
        {
            try
            {
                await _gameInviteManager.DeclineInvite(User.Identity.Name);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DeleteInvite
        [Authorize]
        [HttpDelete("DeleteInvite")]
        public async Task<IActionResult> DeleteInvite()
        {
            try
            {
                await _gameInviteManager.DeleteInvite(User.Identity.Name);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // InviteGame
        [Authorize]
        [HttpPost("InviteGame")]
        public async Task<IActionResult> InviteGame(InviteGameModel inviteGameModel)
        {
            try
            {
                await _gameInviteManager.InviteGame(User.Identity.Name, inviteGameModel.InvitedEmail, inviteGameModel.GameType);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
