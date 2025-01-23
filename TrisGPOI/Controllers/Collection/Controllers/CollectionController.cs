using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrisGPOI.Core.Collection.Interfaces;

namespace TrisGPOI.Controllers.Collection.Controllers
{
    [ApiController]
    [Route("Collection")]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionInventoryManager _collectionInventoryManager;

        public CollectionController(ICollectionInventoryManager collectionInventoryManager)
        {
            _collectionInventoryManager = collectionInventoryManager;
        }

        [Authorize]
        [HttpGet("GetCollection")]
        public async Task<IActionResult> GetCollection()
        {
            try
            {
                var email = User.Identity.Name;
                var collection = await _collectionInventoryManager.GetInventory(email);
                return Ok(new { collection = collection });
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}
