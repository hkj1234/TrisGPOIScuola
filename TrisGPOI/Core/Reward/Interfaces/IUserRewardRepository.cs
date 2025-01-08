namespace TrisGPOI.Core.Reward.Interfaces
{
    public interface IUserRewardRepository
    {
        Task<int> GetRewardRemain(string email);
        Task ResetRewardRemain(string email);
        Task SubtractRewardRemain(string email);
    }
}
