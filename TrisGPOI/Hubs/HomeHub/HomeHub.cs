using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TrisGPOI.Core.User.Interfaces;

namespace TrisGPOI.Hubs.HomeHub
{
    [Authorize]
    public class HomeHub : Hub
    {
        private readonly IUserManager _userManager;
        public HomeHub(IUserManager customerManager)
        {
            _userManager = customerManager;
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                var email = Context.User?.Identity?.Name;
                var status = await _userManager.GetUserStatus(email);
                if (status == "Offline")
                {
                    await base.OnConnectedAsync();
                    await _userManager.ChangeUserStatus(email, "Online");
                }
                else
                {
                    Context.Abort();
                }
            }
            catch (Exception ex) 
            {
                Context.Abort();
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var email = Context.User?.Identity?.Name;
                var status = await _userManager.GetUserStatus(email);
                if (status == "Online")
                    await _userManager.ChangeUserStatus(email, "Offline");
                await base.OnDisconnectedAsync(exception);
            }
            catch (Exception ex)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("Errore", ex.Message);
            }
        }
    }
}
