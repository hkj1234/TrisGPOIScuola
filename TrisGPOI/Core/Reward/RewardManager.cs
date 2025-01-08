using TrisGPOI.Core.Level.Interfaces;
using TrisGPOI.Core.Money.Interfaces;
using TrisGPOI.Core.Reward.Interfaces;

namespace TrisGPOI.Core.Reward
{
    public class RewardManager : IRewardManager
    {
        //TO-DO: da fare comunicazione con il client
        private readonly IMoneyXOManager _moneyXOManager;
        private readonly ILevelManager _levelManager;
        private readonly IUserRewardRepository _userRewardRepository;
        public RewardManager(IMoneyXOManager moneyXOManager, ILevelManager levelManager, IUserRewardRepository userRewardRepository)
        {
            _moneyXOManager = moneyXOManager;
            _levelManager = levelManager;
            _userRewardRepository = userRewardRepository;
        }
        public async Task WinGame(string email, string gameType)
        {
            int rewardRemain = await _userRewardRepository.GetRewardRemain(email);
            int money = 10;
            int experience = 5;
            if (rewardRemain > 0)
            {
                money *= 3;
                experience *= 3;
                await _userRewardRepository.SubtractRewardRemain(email);
            }
            await _moneyXOManager.AddMoney(email, money);
            await _levelManager.GainExperience(email, experience);
        }
        public async Task LoseGame(string email, string gameType)
        {
            int money = 5;
            await _moneyXOManager.AddMoney(email, money);
        }
        public async Task DrawGame(string email, string gameType)
        {
            int money = 10;
            int experience = 5;
            await _moneyXOManager.AddMoney(email, money);
            await _levelManager.GainExperience(email, experience);
        }
    }
}
