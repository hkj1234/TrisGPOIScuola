using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using TrisGPOI.Core.Game.Interfaces;

namespace TrisGPOI.Hubs.Game
{
    [Authorize]
    public class TrisNormaleCPUHub : TrisCPUHubModel
    {
        public TrisNormaleCPUHub(IGameManager gameManager, IGameVictoryManager gameVictoryManager) : base(gameManager, "Normal", gameVictoryManager) { }
    }
}
