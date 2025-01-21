using TrisGPOI.Core.Game.Entities;
using TrisGPOI.Database.Game.Entities;

namespace TrisGPOI.Core.Game.Interfaces
{
    public interface IGameManager
    {
        Task<BoardInfo> CPUPlayMove(string playerEmail);
        Task GameAbandon(string playerEmail);
        Task<BoardInfo> PlayMove(string playerEmail, int position);
        Task JoinGame(string playerEmail, string gameType);
        Task<DBGame?> SearchPlayerPlayingOrWaitingGameAsync(string playerEmail);
        Task<DBGame?> SearchPlayerPlayingGameAsync(string playerEmail);
        Task<DBGame?> SearchGameWithId(string id);
        Task<DBGame?> SearchGameWithId(int id);
        Task CancelSearchGame(string email);
        Task PlayWithCPU(string playerEmail, string type, string difficult);
        Task<DBGame?> GetLastGame(string email);
        Task<bool> CheckWin(string type, string board);
    }
}
