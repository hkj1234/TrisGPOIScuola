namespace TrisGPOI.Core.Reward.Interfaces
{
    public interface IRewardManager
    {
        Task WinGame(string email, string gameType);
        Task LoseGame(string email, string gameType);
        Task DrawGame(string email, string gameType);
    }
}
