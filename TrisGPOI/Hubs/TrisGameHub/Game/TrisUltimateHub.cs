using Microsoft.AspNetCore.Authorization;
using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Core.User.Interfaces;

namespace TrisGPOI.Hubs.TrisGameHub.Game
{
    [Authorize]
    public class TrisUltimateHub : TrisHubModel
    {
        public TrisUltimateHub(IGameManager gameManager, IGameVictoryManager gameVictoryManager, IUserManager userManager) : base(gameManager, "Ultimate", gameVictoryManager, userManager) { }
    }
}
