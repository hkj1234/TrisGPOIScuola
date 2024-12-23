namespace TrisGPOI.Core.Game.Interfaces
{
    public interface IGameVictoryManager
    {
        Task GameFinished(string player1, string player2, string victory, string gameType);
    }
}
