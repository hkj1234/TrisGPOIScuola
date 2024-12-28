using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Core.User.Interfaces;

namespace TrisGPOI.Core.Game
{
    public class GameVictoryManager : IGameVictoryManager
    {
        private readonly IUserVittorieRepository _userVittorieRepository;
        public GameVictoryManager(IUserVittorieRepository userVittorieRepository)
        {
            _userVittorieRepository = userVittorieRepository;
        }

        public async Task GameFinished(string player1, string player2, string victory, string gameType)
        {
            if (player1 == null || player2 == null || victory == null || gameType == null)
            {
                throw new ArgumentNullException();
            }
            if (victory == player1)
            {
                await _userVittorieRepository.UserVictory(player1, gameType);
                await _userVittorieRepository.UserLose(player2, gameType);
            }
            else if (victory == player2)
            {
                await _userVittorieRepository.UserVictory(player2, gameType);
                await _userVittorieRepository.UserLose(player1, gameType);
            }
            else
            {
                await _userVittorieRepository.UserDraw(player1, gameType);
                await _userVittorieRepository.UserDraw(player2, gameType);
            }
        }
    }
}
