using Microsoft.AspNetCore.Authorization;
using TrisGPOI.Core.Game.Interfaces;

namespace TrisGPOI.Hubs.Game
{
    [Authorize]
    public class TrisInfinityHub : TrisHubModel
    {
        public TrisInfinityHub(IGameManager gameManager) : base(gameManager, "Infinity") { }
    }
}





