using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Core.User.Interfaces;

namespace TrisGPOI.Hubs.Game
{
    [Authorize]
    public class TrisNormaleCPUHub : TrisCPUHubModel
    {
        public TrisNormaleCPUHub(IGameManager gameManager, IGameVictoryManager gameVictoryManager, IUserManager userManager) : base(gameManager, "Normal", gameVictoryManager, userManager) { }
    }
}
