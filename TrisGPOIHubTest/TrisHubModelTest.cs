using Microsoft.AspNetCore.SignalR;
using Moq;
using System;
using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Core.User.Interfaces;
using TrisGPOI.Database.Game.Entities;
using TrisGPOI.Hubs.TrisGameHub;

namespace TrisGPOIHubTest
{
    /*
    internal class TrisHubModelTest
    {
        private Mock<IGameManager> _mockGameManager;
        private Mock<IGameVictoryManager> _mockGameVictoryManager;
        private Mock<IUserManager> _mockUserManager;
        private TrisHubModel _hub;

        private Mock<HubCallerContext> _mockHubCallerContext;
        Mock<IHubCallerClients> _mockHubCallerClients;
        Mock<ISingleClientProxy> _mockClientProxy;
        Mock<IGroupManager> _mockGroupManager;

        string email = "test@example.com";
        string connectionId = "connection123";

        [SetUp]
        public void SetUp()
        {
            _mockGameManager = new Mock<IGameManager>(MockBehavior.Strict);
            _mockGameVictoryManager = new Mock<IGameVictoryManager>(MockBehavior.Strict);
            _mockUserManager = new Mock<IUserManager>(MockBehavior.Strict);

            _mockHubCallerContext = new Mock<HubCallerContext>(MockBehavior.Strict);
            _mockHubCallerClients = new Mock<IHubCallerClients>(MockBehavior.Strict);
            _mockClientProxy = new Mock<ISingleClientProxy>(MockBehavior.Strict);
            _mockGroupManager = new Mock<IGroupManager>(MockBehavior.Strict);

            _hub = new TrisHubModel(_mockGameManager.Object, "Normal", _mockGameVictoryManager.Object, _mockUserManager.Object)
            {
                Context = _mockHubCallerContext.Object,
                Clients = _mockHubCallerClients.Object,
                Groups = _mockGroupManager.Object
            };

            _mockHubCallerContext.Setup(c => c.User.Identity.Name).Returns(email);
            _mockHubCallerContext.Setup(c => c.ConnectionId).Returns(connectionId);
            _mockHubCallerContext.Setup(c => c.Abort()).Verifiable();

            _mockHubCallerClients.Setup(clients => clients.Group(It.IsAny<string>())).Returns(_mockClientProxy.Object);
            _mockHubCallerClients.Setup(clients => clients.Client(connectionId)).Returns(_mockClientProxy.Object);

            _mockClientProxy
                .Setup(cp => cp.SendCoreAsync(
                    It.IsAny<string>(),
                    It.IsAny<object[]>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
        }

        [TearDown]
        public void TearDown() 
        {
            _hub.Dispose();
        }

        [Test]
        public async Task Play_ShouldAddUserToGroupAndNotifyClients()
        {
            // Arrange
            
            var game = new DBGame
            {
                Id = 1,
                Player1 = email,
                Player2 = null,
                GameType = "Normal",
                LastMoveTime = DateTime.UtcNow.AddMinutes(-1)
            };

            _mockGameManager.Setup(gm => gm.JoinGame(email, "Normal")).Returns(Task.CompletedTask);
            _mockUserManager.Setup(um => um.ChangeUserStatus(email, "Playing")).Returns(Task.CompletedTask);
            _mockGameManager.Setup(gm => gm.SearchPlayerPlayingOrWaitingGameAsync(email)).ReturnsAsync(game);

            _mockGroupManager.Setup(g => g.AddToGroupAsync(connectionId, game.Id.ToString(), CancellationToken.None)).Returns(Task.CompletedTask);

            // Act
            await _hub.Play();

            // Assert
            _mockGameManager.Verify(gm => gm.JoinGame(email, "Normal"), Times.Once);
            _mockUserManager.Verify(um => um.ChangeUserStatus(email, "Playing"), Times.Once);
            _mockGameManager.Verify(gm => gm.SearchPlayerPlayingOrWaitingGameAsync(email), Times.Once);
            _mockGroupManager.Verify(g => g.AddToGroupAsync(connectionId, game.Id.ToString(), CancellationToken.None), Times.Once);

            _mockClientProxy.Verify(cp =>   cp.SendCoreAsync("Connection",It.Is<object[]>(args => args.Length == 1 && args[0].Equals(email)),It.IsAny<CancellationToken>()), Times.Once);

            _mockClientProxy.Verify(cp => cp.SendCoreAsync("ReceiveMove",It.Is<object[]>(args => args.Length == 1 && args[0].Equals(game)),It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task Play_ShouldAbortContextIfNoGameFound()
        {
            // Arrange
            _mockGameManager.Setup(gm => gm.JoinGame(email, "Normal")).Returns(Task.CompletedTask);
            _mockUserManager.Setup(um => um.ChangeUserStatus(email, "Playing")).Returns(Task.CompletedTask);
            _mockGameManager.Setup(gm => gm.SearchPlayerPlayingOrWaitingGameAsync(email)).ReturnsAsync((DBGame)null);

            // Act
            await _hub.Play();

            // Assert
            _mockGameManager.Verify(gm => gm.JoinGame(email, "Normal"), Times.Once);
            _mockUserManager.Verify(um => um.ChangeUserStatus(email, "Playing"), Times.Once);
            _mockGameManager.Verify(gm => gm.SearchPlayerPlayingOrWaitingGameAsync(email), Times.Once);
            _mockHubCallerContext.Verify(c => c.Abort(), Times.Once);
        }
    }
    */
}
