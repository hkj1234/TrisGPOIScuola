using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TrisGPOI.Core.Home.Interfaces;

namespace TrisGPOI.Hubs.HomeHub
{
    //TODO: aggiornare rispetto al controller, cambiare anche i metodi cambiati nel manager
    [Authorize]
    public class HomeHub : Hub
    {
        private readonly IHomeManager _homeManager;
        public HomeHub(IHomeManager homeManager)
        {
            _homeManager = homeManager;
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                var email = Context.User?.Identity?.Name;
                var status = await _homeManager.GetUserStatus(email);
                if (status == "Offline")
                {
                    await base.OnConnectedAsync();
                    await _homeManager.ChangeUserStatus(email, "Online");
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
                var status = await _homeManager.GetUserStatus(email);
                if (status == "Online")
                    await _homeManager.SetOffline(email);
                await base.OnDisconnectedAsync(exception);
            }
            catch (Exception ex)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("Errore", ex.Message);
            }
        }
    }
}
