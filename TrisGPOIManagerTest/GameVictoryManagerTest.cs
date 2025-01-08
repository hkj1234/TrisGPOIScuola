using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TrisGPOI.Core.Game;
using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Core.Reward.Interfaces;
using TrisGPOI.Core.User.Interfaces;

namespace TrisGPOIManagerTesting
{
    [TestFixture]
    public class GameVictoryManagerTest
    {
        private Mock<IUserVittorieRepository> _mockUserVittorieRepository;
        private Mock<IRewardManager> _mockRewardManager;
        private GameVictoryManager _gameVictoryManager;

        [SetUp]
        public void SetUp()
        {
            _mockUserVittorieRepository = new Mock<IUserVittorieRepository>(MockBehavior.Strict);
            _mockRewardManager = new Mock<IRewardManager>(MockBehavior.Strict);
            _gameVictoryManager = new GameVictoryManager(_mockUserVittorieRepository.Object, _mockRewardManager.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mockUserVittorieRepository.VerifyAll();
        }

        [Test]
        public async Task GameFinished_Player1Wins_CallsVictoryAndLoseCorrectly()
        {
            var player1 = "Player1@example.com";
            var player2 = "Player2@example.com";
            var victory = "Player1@example.com";
            var gameType = "Classic";

            _mockUserVittorieRepository.Setup(repo => repo.UserVictory(player1, gameType)).Returns(Task.CompletedTask);
            _mockUserVittorieRepository.Setup(repo => repo.UserLose(player2, gameType)).Returns(Task.CompletedTask);

            _mockRewardManager.Setup(x => x.WinGame(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);
            _mockRewardManager.Setup(x => x.LoseGame(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);

            await _gameVictoryManager.GameFinished(player1, player2, victory, gameType);

            _mockUserVittorieRepository.Verify(repo => repo.UserVictory(player1, gameType), Times.Once);
            _mockUserVittorieRepository.Verify(repo => repo.UserLose(player2, gameType), Times.Once);
        }

        [Test]
        public async Task GameFinished_Player2Wins_CallsVictoryAndLoseCorrectly()
        {
            var player1 = "Player1@example.com";
            var player2 = "Player2@example.com";
            var victory = "Player2@example.com";
            var gameType = "Classic";

            _mockUserVittorieRepository.Setup(repo => repo.UserVictory(player2, gameType)).Returns(Task.CompletedTask);
            _mockUserVittorieRepository.Setup(repo => repo.UserLose(player1, gameType)).Returns(Task.CompletedTask);

            _mockRewardManager.Setup(x => x.WinGame(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);
            _mockRewardManager.Setup(x => x.LoseGame(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);

            await _gameVictoryManager.GameFinished(player1, player2, victory, gameType);

            _mockUserVittorieRepository.Verify(repo => repo.UserVictory(player2, gameType), Times.Once);
            _mockUserVittorieRepository.Verify(repo => repo.UserLose(player1, gameType), Times.Once);
        }

        [Test]
        public async Task GameFinished_Draw_CallsDrawCorrectly()
        {
            var player1 = "Player1@example.com";
            var player2 = "Player2@example.com";
            var victory = "Draw";
            var gameType = "Classic";

            _mockUserVittorieRepository.Setup(repo => repo.UserDraw(player1, gameType)).Returns(Task.CompletedTask);
            _mockUserVittorieRepository.Setup(repo => repo.UserDraw(player2, gameType)).Returns(Task.CompletedTask);

            _mockRewardManager.Setup(x => x.DrawGame(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);

            await _gameVictoryManager.GameFinished(player1, player2, victory, gameType);

            _mockUserVittorieRepository.Verify(repo => repo.UserDraw(player1, gameType), Times.Once);
            _mockUserVittorieRepository.Verify(repo => repo.UserDraw(player2, gameType), Times.Once);
        }

        [Test]
        public void GameFinished_InvalidInputs_ThrowsArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _gameVictoryManager.GameFinished(null, "Player2", "Player1", "Classic"));
            Assert.ThrowsAsync<ArgumentNullException>(() => _gameVictoryManager.GameFinished("Player1", null, "Player1", "Classic"));
            Assert.ThrowsAsync<ArgumentNullException>(() => _gameVictoryManager.GameFinished("Player1", "Player2", null, "Classic"));
            Assert.ThrowsAsync<ArgumentNullException>(() => _gameVictoryManager.GameFinished("Player1", "Player2", "Player1", null));
        }
    }
}
