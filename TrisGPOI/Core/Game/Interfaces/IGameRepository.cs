using TrisGPOI.Database.Game.Entities;

namespace TrisGPOI.Core.Game.Interfaces
{
    public interface IGameRepository
    {
        Task<DBGame?> SearchPlayerPlayingOrWaitingGame(string email);
        Task<bool> JoinSomeGame(string typeGame, string emailPlayer2);
        Task StartJoinGame(string typeGame, string emailPlayer1);
        Task<string> PlayMove(string email, int position);
        Task<string> GameFinished(int id);
        Task CancelSearchGame(string email);
        Task<DBGame?> SearchPlayerPlayingGame(string email);
    }
}
