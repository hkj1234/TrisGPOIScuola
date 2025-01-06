using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrisGPOI.Controllers.ReceiveBox.Entities;
using TrisGPOI.Core.ReceiveBox.Interfaces;

namespace TrisGPOI.Controllers.ReceiveBox
{
    [ApiController]
    [Route("ReceiveBox")]
    public class ReceiveBoxController : ControllerBase
    {
        private readonly IReceiveBoxManager _receiveBoxManager;
        public ReceiveBoxController(IReceiveBoxManager receiveBoxManager)
        {
            _receiveBoxManager = receiveBoxManager;
        }

        //GetMailBox
        [Authorize]
        [HttpGet("GetMailBox")]
        public async Task<IActionResult> GetMailBox()
        {
            try
            {
                var email = User?.Identity?.Name;
                var mailBox = await _receiveBoxManager.GetReceiveBox(email);
                return Ok(new 
                {
                    mailBox = mailBox
                });
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }

        //ReadMailBox
        [Authorize]
        [HttpPut("ReadMailBox")]
        public async Task<IActionResult> ReadMailBox([FromBody] ReceiveBoxRequest request)
        {
            try
            {
                await _receiveBoxManager.ReadReceiveBox(request.id);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }

        //DeleteMailBox
        [Authorize]
        [HttpDelete("DeleteMailBox")]
        public async Task<IActionResult> DeleteMailBox([FromBody] ReceiveBoxRequest request)
        {
            try
            {
                await _receiveBoxManager.DeleteReceiveBox(request.id);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }

        //UnreadMailBox
        [Authorize]
        [HttpGet("ExistUnreadMailBox")]
        public async Task<IActionResult> ExistUnreadMailBox()
        {
            try
            {
                var email = User?.Identity?.Name;
                var unreadMailBox = await _receiveBoxManager.ExistUnreadMailBox(email);
                return Ok(new { 
                    unreadMailBox = unreadMailBox
                });
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }   
    }
}
