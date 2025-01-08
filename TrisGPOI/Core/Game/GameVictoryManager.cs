using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Core.Reward.Interfaces;
using TrisGPOI.Core.User.Interfaces;

namespace TrisGPOI.Core.Game
{
    public class GameVictoryManager : IGameVictoryManager
    {
        private readonly IUserVittorieRepository _userVittorieRepository;
        private readonly IRewardManager _rewardManager;
        public GameVictoryManager(IUserVittorieRepository userVittorieRepository, IRewardManager rewardManager)
        {
            _userVittorieRepository = userVittorieRepository;
            _rewardManager = rewardManager;
        }

        public async Task GameFinished(string player1, string player2, string victory, string gameType)
        {
            if (player1 == null || player2 == null || victory == null || gameType == null)
            {
                throw new ArgumentNullException();
            }
            if (victory == player1)
            {
                await WinGame(player1, gameType);
                await LoseGame(player2, gameType);
            }
            else if (victory == player2)
            {
                await WinGame(player2, gameType);
                await LoseGame(player1, gameType);
            }
            else
            {
                await DrawGame(player1, gameType);
                await DrawGame(player2, gameType);
            }
        }

        public async Task WinGame(string email, string gameType)
        {
            if (email.Contains("@"))
            {
                await _rewardManager.WinGame(email, gameType);
                await _userVittorieRepository.UserVictory(email, gameType);
            }
        }

        public async Task LoseGame(string email, string gameType)
        {
            if (email.Contains("@"))
            {
                await _rewardManager.LoseGame(email, gameType);
                await _userVittorieRepository.UserLose(email, gameType);
            }
        }

        public async Task DrawGame(string email, string gameType)
        {
            if (email.Contains("@"))
            {
                await _rewardManager.DrawGame(email, gameType);
                await _userVittorieRepository.UserDraw(email, gameType);
            }
        }
    }
}
