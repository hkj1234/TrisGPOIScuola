using TrisGPOI.Database.Game.Entities;

namespace TrisGPOI.Core.Game.Interfaces
{
    public interface IGameRepository
    {
        Task<DBGame?> SearchPlayerPlayingOrWaitingGame(string email);
        Task<bool> JoinSomeGame(string typeGame, string emailPlayer2);
        Task StartJoinGame(string typeGame, string emailPlayer1);
        Task UpdateBoard(string email, string board);
        Task<string> GameFinished(int id);
        Task CancelSearchGame(string email);
        Task<DBGame?> SearchPlayerPlayingGame(string email);
        Task StartJoinCPUGame(string typeGame, string emailPlayer1, string Difficult);
    }
}
