using Microsoft.AspNetCore.SignalR;

namespace TrisGPOI.Hubs
{
    public class TimerStateForTimeOut
    {
        public IHubCallerClients Clients { get; set; }
        public string groupName {  get; set; }
    }
}
