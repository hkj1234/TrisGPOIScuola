using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TrisGPOI.Core.Home.Interfaces;

namespace TrisGPOI.Controllers.Home.Controller
{
    [ApiController]
    [Route("Home")]
    public class HomeController : ControllerBase
    {
        private readonly IHomeManager _homeManager;
        private readonly IGameInviteManager _gameInviteManager;
        public HomeController(IHomeManager homeManager, IGameInviteManager gameInviteManager)
        {
            _homeManager = homeManager;
            _gameInviteManager = gameInviteManager;
        }

        //SetOnline
        [Authorize]
        [HttpPut("SetOnline")]
        public async Task<IActionResult> SetOnline()
        {
            try
            {
                string email = User.Identity.Name;
                await _homeManager.SetOnlineTemperaly(email);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //GetInvites
        [Authorize]
        [HttpGet("GetInvites")]
        public async Task<IActionResult> GetInvites()
        {
            try
            {
                return Ok(await _gameInviteManager.GetInvitesByEmail(User.Identity.Name));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //Invite
        [Authorize]
        [HttpPost("Invite")]
        public async Task<IActionResult> Invite(string invitedEmail, string gameType)
        {
            try
            {
                await _gameInviteManager.InviteGame(User.Identity.Name, invitedEmail, gameType);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //AcceptInvite
        [Authorize]
        [HttpPost("AcceptInvite")]
        public async Task<IActionResult> AcceptInvite()
        {
            try
            {
                string invitedEmail = User.Identity.Name;
                await _gameInviteManager.AcceptInvite(invitedEmail);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //DeclineInvite
        [Authorize]
        [HttpPost("DeclineInvite")]
        public async Task<IActionResult> DeclineInvite()
        {
            try
            {
                string invitedEmail = User.Identity.Name;
                await _gameInviteManager.DeclineInvite(invitedEmail);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
