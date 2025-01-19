using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrisGPOI.Controllers.Shop.Entities;
using TrisGPOI.Core.Shop.Interfaces;

namespace TrisGPOI.Controllers.Shop.Controllers
{
    [ApiController]
    [Route("Shop")]
    public class ShopController : ControllerBase
    {
        private readonly IShopManager _shopManager;
        public ShopController(IShopManager shopManager)
        {
            _shopManager = shopManager;
        }

        [Authorize]
        [HttpGet("GetShops")]
        public async Task<IActionResult> GetShops()
        {
            try 
            {
                var email = User.Identity.Name;
                var shops = await _shopManager.GetShops(email);
                return Ok(shops);
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [Authorize]
        [HttpPost("PurchasedShop")]
        public async Task<IActionResult> PurchasedShop(PurchasedShopRequest request)
        {
            try 
            {
                var email = User.Identity.Name;
                await _shopManager.PurchasedShop(email, request.Position);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }
    }
}
