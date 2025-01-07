using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrisGPOI.Controllers.Friend.Entities;
using TrisGPOI.Core.Friend.Interfaces;

namespace TrisGPOI.Controllers.Friend.Controllers
{
    [ApiController]
    [Route("Friend")]
    public class FriendController : ControllerBase
    {
        private readonly IFriendManager _friendManager;
        public FriendController(IFriendManager friendManager)
        {
            _friendManager = friendManager;
        }

        //GetFriends
        [Authorize]
        [HttpGet("GetFriends")]
        public async Task<IActionResult> GetFriends()
        {
            try
            {
                var email = User?.Identity?.Name;
                var friends = await _friendManager.GetFriends(email);
                return Ok(new 
                {
                    friends = friends
                });
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }

        //GetFriendsRequest
        [Authorize]
        [HttpGet("GetFriendsRequest")]
        public async Task<IActionResult> GetFriendsRequest()
        {
            try
            {
                var email = User?.Identity?.Name;
                var friendsRequest = await _friendManager.GetFriendsRequest(email);
                return Ok(new 
                {
                    friendsRequest = friendsRequest
                });
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }

        //GetFriendsRequestSent
        [Authorize]
        [HttpGet("GetFriendsRequestSent")]
        public async Task<IActionResult> GetFriendsRequestSent()
        {
            try
            {
                var email = User?.Identity?.Name;
                var friendsRequestSent = await _friendManager.GetFriendsRequestSent(email);
                return Ok(new 
                {
                    friendsRequestSent = friendsRequestSent
                });
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }
    
        //SendFriendRequest 
        [Authorize]
        [HttpPost("SendFriendRequestByEmail")]
        public async Task<IActionResult> SendFriendRequestByEmail([FromBody] FriendEmailRequest friendEmailRequest)
        {
            try
            {
                var email = User?.Identity?.Name;
                await _friendManager.SendFriendRequestByEmail(email, friendEmailRequest.email);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }

        //AcceptFriendRequest
        [Authorize]
        [HttpPost("AcceptFriendRequest")]   
        public async Task<IActionResult> AcceptFriendRequest([FromBody] FriendEmailRequest friendEmailRequest)
        {
            try
            {
                var email = User?.Identity?.Name;
                await _friendManager.AcceptFriendRequest(email, friendEmailRequest.email);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }

        //RejectFriendRequest
        [Authorize]
        [HttpPost("RejectFriendRequest")]
        public async Task<IActionResult> RejectFriendRequest([FromBody] FriendEmailRequest friendEmailRequest)
        {
            try
            {
                var email = User?.Identity?.Name;
                await _friendManager.RejectFriendRequest(email, friendEmailRequest.email);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }

        //RemoveFriend
        [Authorize]
        [HttpDelete("RemoveFriend")]
        public async Task<IActionResult> RemoveFriend([FromBody] FriendEmailRequest friendEmailRequest)
        {
            try
            {
                var email = User?.Identity?.Name;
                await _friendManager.RemoveFriend(email, friendEmailRequest.email);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }

        //SendFriendRequestByUsername
        [Authorize]
        [HttpPost("SendFriendRequestByUsername")]
        public async Task<IActionResult> SendFriendRequestByUsername([FromBody] FriendUsernameRequest friendUsernameRequest)
        {
            try
            {
                var email = User?.Identity?.Name;
                await _friendManager.SendFriendRequestByUsername(email, friendUsernameRequest.username);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound($"Resource not found {e.Message}");
            }
        }
    }
}
