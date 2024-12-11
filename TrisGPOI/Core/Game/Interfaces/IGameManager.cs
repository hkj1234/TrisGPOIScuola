using TrisGPOI.Core.Game.Entities;
using TrisGPOI.Database.Game.Entities;

namespace TrisGPOI.Core.Game.Interfaces
{
    public interface IGameManager
    {
        Task<BoardInfo> PlayMove(string playerEmail, int position);
        Task JoinGame(string playerEmail, string gameType);
        Task<DBGame?> SearchPlayerPlayingOrWaitingGameAsync(string playerEmail);
        Task CancelSearchGame(string email);
    }
}
