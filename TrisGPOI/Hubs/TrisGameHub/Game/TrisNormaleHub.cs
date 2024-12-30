using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TrisGPOI.Core.Game;
using TrisGPOI.Core.Game.Entities;
using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Core.User.Interfaces;
using TrisGPOI.Hubs.TrisGameHub;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TrisGPOI.Hubs.TrisGameHub.Game
{
    [Authorize]
    public class TrisNormaleHub : TrisHubModel
    {
        public TrisNormaleHub(IGameManager gameManager, IGameVictoryManager gameVictoryManager, IUserManager userManager) : base(gameManager, "Normal", gameVictoryManager, userManager) { }
    }
}





