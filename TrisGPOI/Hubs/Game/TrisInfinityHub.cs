using Microsoft.AspNetCore.Authorization;
using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Core.User.Interfaces;

namespace TrisGPOI.Hubs.Game
{
    [Authorize]
    public class TrisInfinityHub : TrisHubModel
    {
        public TrisInfinityHub(IGameManager gameManager, IGameVictoryManager gameVictoryManager, IUserManager userManager) : base(gameManager, "Infinity", gameVictoryManager, userManager) { }
    }
}





