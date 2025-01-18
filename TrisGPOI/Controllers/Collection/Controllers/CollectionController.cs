using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrisGPOI.Core.Collection.Interfaces;

namespace TrisGPOI.Controllers.Collection.Controllers
{
    [ApiController]
    [Route("Collection")]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionInventoryRepository _collectionInventoryRepository;

        public CollectionController(ICollectionInventoryRepository collectionInventoryRepository)
        {
            _collectionInventoryRepository = collectionInventoryRepository;
        }

        [Authorize]
        [HttpGet("GetCollection")]
        public async Task<IActionResult> GetCollection()
        {
            try
            {
                var email = User.Identity.Name;
                var collection = await _collectionInventoryRepository.GetInventory(email);
                return Ok(new { collection = collection });
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}
