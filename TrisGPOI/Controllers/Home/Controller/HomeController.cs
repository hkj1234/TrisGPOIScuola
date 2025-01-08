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
        public HomeController(IHomeManager homeManager)
        {
            _homeManager = homeManager;
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
    }
}
