using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TrisGPOI.Core.Home.Interfaces;
using TrisGPOI.Core.Game.Interfaces;

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
                var status = await _homeManager.GetUserStatus(email);
                var isInvited = await _gameInviteManager.IsInvited(email);
                var isInviter = await _gameInviteManager.IsInviter(email);
                return Ok(new {
                    Status = status,
                    IsInvited = isInvited,
                    IsInviter = isInviter
                });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
