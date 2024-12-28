using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Threading.Tasks;
using TrisGPOI.Core.Game.Entities;
using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Hubs;
using TrisGPOI.Database.Game.Entities;

[TestFixture]
public class TrisHubModelTests
{
    private Mock<IGameManager> _mockGameManager;
    private Mock<IGameVictoryManager> _mockGameVictoryManager;
    private Mock<ISingleClientProxy> _mockClientProxy;
    private Mock<IHubCallerClients> _mockClients;
    private Mock<HubCallerContext> _mockContext;
    private TrisHubModel? _hub;

    [SetUp]
    public void Setup()
    {
        _mockGameManager = new Mock<IGameManager>();
        _mockGameVictoryManager = new Mock<IGameVictoryManager>();
        _mockClientProxy = new Mock<ISingleClientProxy>();
        _mockClients = new Mock<IHubCallerClients>();
        _mockContext = new Mock<HubCallerContext>();

        _mockClients.Setup(clients => clients.Client(It.IsAny<string>())).Returns(_mockClientProxy.Object);
        _mockClients.Setup(clients => clients.Group(It.IsAny<string>())).Returns(_mockClientProxy.Object);

        _mockContext.Setup(context => context.User.Identity.Name).Returns("player@example.com");

        _hub = new TrisHubModel(_mockGameManager.Object, "Normal", _mockGameVictoryManager.Object)
        {
            Clients = _mockClients.Object,
            Context = _mockContext.Object
        };
    }

    [TearDown]
    public void TearDown()
    {
        _hub?.Dispose();
    }


    [Test]
    public async Task OnConnectedAsync_ValidUser_AddsToGroup()
    {
        // Arrange
        var game = new DBGame
        {
            Id = 1,
            Player1 = "player@example.com",
            GameType = "Normal"
        };

        var mockGroupClientProxy = new Mock<IClientProxy>();

        _mockGameManager.Setup(m => m.SearchPlayerPlayingOrWaitingGameAsync("player@example.com"))
            .ReturnsAsync(game);

        _mockClients.Setup(clients => clients.Group("1"))
            .Returns(mockGroupClientProxy.Object);

        // Act
        await _hub.OnConnectedAsync();

        // Assert
        _mockGameManager.Verify(m => m.SearchPlayerPlayingOrWaitingGameAsync("player@example.com"), Times.Once);

        mockGroupClientProxy.Verify(client => client.SendCoreAsync(
            "Connection",
            It.Is<object[]>(args => args.Length == 1 && (string)args[0] == "player@example.com"),
            default), Times.Once);
    }



    [Test]
    public async Task SendMove_ValidMove_SendsMoveToGroup()
    {
        // Arrange
        var game = new DBGame { Id = 1 };
        var boardInfo = new BoardInfo { Victory = '-', LastMoveTime = System.DateTime.UtcNow };

        _mockGameManager.Setup(m => m.SearchPlayerPlayingGameAsync("player@example.com")).ReturnsAsync(game);
        _mockGameManager.Setup(m => m.PlayMove("player@example.com", 0)).ReturnsAsync(boardInfo);

        // Act
        await _hub.SendMove(0);

        // Assert
        _mockClients.Verify(clients => clients.Group("1").SendAsync("ReceiveMove", boardInfo, default), Times.Once);
    }

    [Test]
    public async Task Abandon_PlayerAbandons_NotifiesWinner()
    {
        // Arrange
        var game = new DBGame
        {
            Id = 1,
            Player1 = "player@example.com",
            Player2 = "opponent@example.com"
        };

        _mockGameManager.Setup(m => m.SearchPlayerPlayingGameAsync("player@example.com")).ReturnsAsync(game);

        // Crea un mock esplicito di IClientProxy
        var mockClientProxy = new Mock<ISingleClientProxy>();

        // Configura il mock per verificare la chiamata a SendAsync
        mockClientProxy.Setup(client => client.SendCoreAsync(It.IsAny<string>(), It.IsAny<object[]>(), It.IsAny<CancellationToken>()))
            .Verifiable();

        // Configura il mock Clients per restituire il mockClientProxy
        _mockClients.Setup(clients => clients.Client(It.IsAny<string>())).Returns(mockClientProxy.Object);

        // Act
        await _hub.Abandon();

        // Assert
        // Verifica che SendCoreAsync sia stato chiamato con i parametri corretti
        mockClientProxy.Verify(client =>
        client.SendCoreAsync("Winning", It.Is<object[]>(o => o.Length == 1 && (string)o[0] == "opponent@example.com"), It.IsAny<CancellationToken>()),
        Times.Once);
    }
}
