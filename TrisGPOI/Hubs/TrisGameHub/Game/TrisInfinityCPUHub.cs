using Microsoft.AspNetCore.Authorization;
using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Core.User.Interfaces;

namespace TrisGPOI.Hubs.TrisGameHub.Game
{
    [Authorize]
    public class TrisInfinityCPUHub : TrisCPUHubModel
    {
        public TrisInfinityCPUHub(IGameManager gameManager, IGameVictoryManager gameVictoryManager, IUserManager userManager) : base(gameManager, "Infinity", gameVictoryManager, userManager) { }
    }
}
