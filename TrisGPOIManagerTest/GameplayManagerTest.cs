using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrisGPOI.Core.CPU.Interfaces;
using TrisGPOI.Core.Game.Exceptions;
using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Core.Game;
using TrisGPOI.Database.Game.Entities;

namespace TrisGPOIManagerTesting
{
    internal class GameplayManagerTest
    {
        private Mock<IGameRepository> _mockGameRepository;
        private Mock<ITrisManagerFabric> _mockTrisManagerFabric;
        private Mock<ICPUManagerFabric> _mockCPUManagerFabric;
        private GameplayManager _gameplayManager;

        [SetUp]
        public void SetUp()
        {
            _mockGameRepository = new Mock<IGameRepository>(MockBehavior.Strict);
            _mockTrisManagerFabric = new Mock<ITrisManagerFabric>(MockBehavior.Strict);
            _mockCPUManagerFabric = new Mock<ICPUManagerFabric>(MockBehavior.Strict);
            _gameplayManager = new GameplayManager(_mockGameRepository.Object, _mockTrisManagerFabric.Object, _mockCPUManagerFabric.Object);
        }

        [Test]
        public void PlayMove_NoGamePlaying_ThrowsNoGamePlayingException()
        {
            _mockGameRepository.Setup(repo => repo.SearchPlayerPlayingGame(It.IsAny<string>()))
                .ReturnsAsync((DBGame)null);

            Assert.ThrowsAsync<NoGamePlayingException>(() => _gameplayManager.PlayMove("player@example.com", 0));
        }

        [Test]
        public void PlayMove_InvalidPlayerMove_ThrowsInvalidPlayerMoveException()
        {
            var game = new DBGame
            {
                CurrentPlayer = "otherplayer@example.com",
                Board = "---------",
                Player1 = "player1@example.com",
                Player2 = "player2@example.com",
                GameType = "Normal"
            };
            _mockGameRepository.Setup(repo => repo.SearchPlayerPlayingGame("player@example.com"))
                .ReturnsAsync(game);

            Assert.ThrowsAsync<InvalidPlayerMoveException>(() => _gameplayManager.PlayMove("player@example.com", 0));
        }

        [Test]
        public async Task PlayMove_ValidMove_ReturnsBoardInfo()
        {
            var game = new DBGame
            {
                CurrentPlayer = "player@example.com",
                Board = "---------",
                Player1 = "player@example.com",
                Player2 = "otherplayer@example.com",
                GameType = "Normal"
            };
            _mockGameRepository.Setup(repo => repo.SearchPlayerPlayingGame("player@example.com"))
                .ReturnsAsync(game);

            var trisManagerMock = new Mock<ITrisManager>();
            trisManagerMock.Setup(m => m.PlayMove(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<char>()))
                .Returns("X--------");
            trisManagerMock.Setup(m => m.CheckWin(It.IsAny<string>()))
                .Returns('-');

            _mockTrisManagerFabric.Setup(fabric => fabric.CreateTrisManager(game.GameType))
                .Returns(trisManagerMock.Object);

            _mockGameRepository.Setup(repo => repo.UpdateBoard(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new DBGame { LastMoveTime = DateTime.UtcNow });

            var result = await _gameplayManager.PlayMove("player@example.com", 0);

            Assert.IsNotNull(result);
            Assert.AreEqual("X--------", result.Board);
            Assert.AreEqual('-', result.Victory);
            Assert.AreEqual(game.Player2, result.CurrentPlayer);
        }

        [Test]
        public async Task CPUPlayMove_ValidMove_ReturnsBoardInfo()
        {
            var game = new DBGame
            {
                CurrentPlayer = "player@example.com",
                Board = "---------",
                Player1 = "player@example.com",
                Player2 = "CPU",
                GameType = "Normal"
            };
            _mockGameRepository.Setup(repo => repo.SearchPlayerPlayingGame("player@example.com"))
                .ReturnsAsync(game);

            var trisManagerMock = new Mock<ITrisManager>();
            trisManagerMock.Setup(m => m.PlayMove(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<char>()))
                .Returns("-X-------");
            trisManagerMock.Setup(m => m.CheckWin(It.IsAny<string>()))
                .Returns('-');

            var cpuManagerMock = new Mock<ICPUManager>();
            cpuManagerMock.Setup(cpu => cpu.GetCPUMove(It.IsAny<string>()))
                .Returns(1);

            var TypeCPUManagerFabric = new Mock<ITypeCPUManagerFabric>();
            TypeCPUManagerFabric.Setup(x => x.CreateCPUManager(It.IsAny<string>())).Returns(cpuManagerMock.Object);

            _mockCPUManagerFabric.Setup(fabric => fabric.CreateTypeCPUFabric(It.IsAny<string>()))
                .Returns(TypeCPUManagerFabric.Object);

            _mockTrisManagerFabric.Setup(fabric => fabric.CreateTrisManager(game.GameType))
                .Returns(trisManagerMock.Object);

            _mockGameRepository.Setup(repo => repo.UpdateBoard(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new DBGame { LastMoveTime = DateTime.UtcNow });

            var result = await _gameplayManager.CPUPlayMove("player@example.com");

            Assert.IsNotNull(result);
            Assert.AreEqual("-X-------", result.Board);
            Assert.AreEqual('-', result.Victory);
        }

        [Test]
        public void GameAbandon_NoGamePlaying_ThrowsNoGamePlayingException()
        {
            _mockGameRepository.Setup(repo => repo.GetLastGame(It.IsAny<string>()))
                .ReturnsAsync((DBGame)null);

            Assert.ThrowsAsync<NoGamePlayingException>(() => _gameplayManager.GameAbandon("player@example.com"));
        }

        [Test]
        public async Task GameAbandon_ValidGame_FinishesGame()
        {
            var game = new DBGame
            {
                Id = 1,
                CurrentPlayer = "player@example.com",
                Board = "12121212-",
                Player1 = "player@example.com",
                Player2 = "otherplayer@example.com",
                GameType = "Normal",
                Winning = '-',
            };

            _mockGameRepository.Setup(repo => repo.GetLastGame("player@example.com"))
                .ReturnsAsync(game);

            _mockGameRepository.Setup(repo => repo.GameFinished(game.Id, '2')).ReturnsAsync("Ciao");

            await _gameplayManager.GameAbandon("player@example.com");

            _mockGameRepository.Verify(repo => repo.GameFinished(game.Id, '2'), Times.Once);
        }

        [Test]
        public void JoinGame_InvalidGameType_ThrowsInvalidGameTypeException()
        {
            Assert.ThrowsAsync<InvalidGameTypeException>(() => _gameplayManager.JoinGame("player@example.com", "InvalidType"));
        }

        [Test]
        public void JoinGame_PlayerAlreadyInGame_ThrowsExistGameException()
        {
            _mockGameRepository.Setup(repo => repo.SearchPlayerPlayingOrWaitingGame(It.IsAny<string>()))
                .ReturnsAsync(new DBGame());

            Assert.ThrowsAsync<ExistGameException>(() => _gameplayManager.JoinGame("player@example.com", "Normal"));
        }

        [Test]
        public async Task JoinGame_ValidGameType_JoinsOrCreatesNewGame()
        {
            _mockGameRepository.Setup(repo => repo.SearchPlayerPlayingOrWaitingGame(It.IsAny<string>()))
                .ReturnsAsync((DBGame)null);

            _mockGameRepository.Setup(repo => repo.JoinSomeGame(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            var trisManagerMock = new Mock<ITrisManager>();
            trisManagerMock.Setup(m => m.CreateEmptyBoard()).Returns("---------");

            _mockTrisManagerFabric.Setup(fabric => fabric.CreateTrisManager(It.IsAny<string>()))
                .Returns(trisManagerMock.Object);

            _mockGameRepository.Setup(repo => repo.StartJoinGame(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            await _gameplayManager.JoinGame("player@example.com", "Normal");

            _mockGameRepository.Verify(repo => repo.StartJoinGame("Normal", "player@example.com", "---------"), Times.Once);
        }

        [Test]
        public void PlayWithCPU_InvalidGameType_ThrowsInvalidGameTypeException()
        {
            var invalidGameType = "InvalidType";

            Assert.ThrowsAsync<InvalidGameTypeException>(() => _gameplayManager.PlayWithCPU("player@example.com", invalidGameType, "Easy"));
        }

        [Test]
        public void PlayWithCPU_PlayerAlreadyInGame_ThrowsExistGameException()
        {
            _mockGameRepository.Setup(repo => repo.SearchPlayerPlayingOrWaitingGame("player@example.com"))
                .ReturnsAsync(new DBGame());

            Assert.ThrowsAsync<ExistGameException>(() => _gameplayManager.PlayWithCPU("player@example.com", "Normal", "Medium"));
        }

        [Test]
        public async Task PlayWithCPU_ValidInputs_StartsGame()
        {
            _mockGameRepository.Setup(repo => repo.SearchPlayerPlayingOrWaitingGame("player@example.com"))
                .ReturnsAsync((DBGame)null);

            var trisManagerMock = new Mock<ITrisManager>();
            trisManagerMock.Setup(m => m.CreateEmptyBoard())
                .Returns("---------");

            _mockTrisManagerFabric.Setup(fabric => fabric.CreateTrisManager("Normal"))
                .Returns(trisManagerMock.Object);

            _mockGameRepository.Setup(repo => repo.StartJoinCPUGame("Normal", "player@example.com", "Hard", "---------"))
                .Returns(Task.CompletedTask);

            await _gameplayManager.PlayWithCPU("player@example.com", "Normal", "Hard");

            _mockGameRepository.Verify(repo => repo.StartJoinCPUGame("Normal", "player@example.com", "Hard", "---------"), Times.Once);
        }

    }
}
