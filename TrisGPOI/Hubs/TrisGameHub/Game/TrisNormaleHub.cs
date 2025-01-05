using Microsoft.AspNetCore.Authorization;
using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Core.User.Interfaces;

namespace TrisGPOI.Hubs.TrisGameHub.Game
{
    [Authorize]
    public class TrisNormaleHub : TrisHubModel
    {
        public TrisNormaleHub(IGameManager gameManager, IGameVictoryManager gameVictoryManager, IUserManager userManager) : base(gameManager, "Normal", gameVictoryManager, userManager) { }
    }
}





