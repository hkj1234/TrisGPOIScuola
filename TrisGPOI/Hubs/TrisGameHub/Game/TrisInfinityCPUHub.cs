using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Core.User.Interfaces;
using TrisGPOI.Hubs.TrisGameHub;

namespace TrisGPOI.Hubs.TrisGameHub.Game
{
    [Authorize]
    public class TrisInfinityCPUHub : TrisCPUHubModel
    {
        public TrisInfinityCPUHub(IGameManager gameManager, IGameVictoryManager gameVictoryManager, IUserManager userManager) : base(gameManager, "Infinity", gameVictoryManager, userManager) { }
    }
}
