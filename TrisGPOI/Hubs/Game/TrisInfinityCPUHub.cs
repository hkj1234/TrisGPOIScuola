using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using TrisGPOI.Core.Game.Interfaces;

namespace TrisGPOI.Hubs.Game
{
    [Authorize]
    public class TrisInfinityCPUHub : TrisCPUHubModel
    {
        public TrisInfinityCPUHub(IGameManager gameManager, IGameVictoryManager gameVictoryManager) : base(gameManager, "Infinity", gameVictoryManager) { }
    }
}
