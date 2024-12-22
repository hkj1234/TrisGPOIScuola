using Microsoft.AspNetCore.SignalR;
using Org.BouncyCastle.Asn1.Cms;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using TrisGPOI.Core.Game.Interfaces;

namespace TrisGPOI.Hubs
{
    public class TrisCPUHubModel : TrisHubModel
    {
        public TrisCPUHubModel(IGameManager gameManager, string type) : base(gameManager, type) { }
        public override async Task OnConnectedAsync()
        {
            var email = Context.User?.Identity?.Name;
            try
            {
                var game = await _gameManager.SearchPlayerPlayingOrWaitingGameAsync(email);
                string connectionId = Context.ConnectionId;

                if (game != null && game.GameType == _type && (game.Player2 != null && !game.Player2.Contains('@')))
                {
                    string groupName = game.Id.ToString();
                    // Aggiungi la nuova connessione
                    await Groups.AddToGroupAsync(connectionId, groupName);
                    await base.OnConnectedAsync();
                    await Clients.Group(groupName).SendAsync("Connection", email);
                    await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMove", arg1: game);
                    updateTimer(groupName, game.LastMoveTime);
                }
                else
                {
                    await base.OnConnectedAsync();
                }
            }
            catch (Exception ex)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("Errore", ex.Message);
                Context.Abort();
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var email = Context.User?.Identity?.Name;
                var game = await _gameManager.SearchPlayerPlayingOrWaitingGameAsync(email);
                if (game != null)
                {
                    string groupName = game.Id.ToString();
                    await Clients.Group(groupName).SendAsync("Disconnection", email);
                }
                await _gameManager.CancelSearchGame(email);
                await base.OnDisconnectedAsync(exception);
            }
            catch (Exception ex)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("Errore", ex.Message);
            }
        }

        public async Task PlayWithCPU(string Difficult)
        {
            try
            {
                var email = Context.User?.Identity?.Name;
                await _gameManager.PlayWithCPU(email, _type, Difficult);

                var game = await _gameManager.SearchPlayerPlayingOrWaitingGameAsync(email);
                string connectionId = Context.ConnectionId;

                if (game != null && game.GameType == _type && (game.Player2 != null && !game.Player2.Contains('@')))
                {
                    string groupName = game.Id.ToString();
                    // Aggiungi la nuova connessione
                    await Groups.AddToGroupAsync(connectionId, groupName);
                    await Clients.Group(groupName).SendAsync("Connection", email);
                    await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMove", arg1: game);
                    updateTimer(groupName, game.LastMoveTime);
                }
            }
            catch (Exception ex)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("Errore", ex.Message);
            }
        }

        public override async Task SendMove(int position)
        {
            try
            {
                var email = Context.User?.Identity?.Name;
                var game = await _gameManager.SearchPlayerPlayingOrWaitingGameAsync(email);
                var board = await _gameManager.PlayMove(email, position);
                string groupName = game.Id.ToString();
                // Invia la mossa a tutti i client
                await Clients.Group(groupName).SendAsync("ReceiveMove", board);

                //timer
                if (board.Victory == '-')
                {
                    updateTimer(groupName, board.LastMoveTime);
                }

                await Task.Delay(10);

                //se il giocatore vince manda un messaggio
                await CheckComunicationWinning(board.Victory, email, groupName);

                if (board.Victory == '-' && (!game.Player2.Contains(value: "@")))
                {
                    board = await _gameManager.CPUPlayMove(email);
                    await Clients.Group(groupName).SendAsync("ReceiveMove", board);

                    //timer
                    if (board.Victory == '-')
                    {
                        updateTimer(groupName, board.LastMoveTime);
                    }

                    await Task.Delay(10);

                    //se il AI vince manda un messaggio
                    await CheckComunicationWinning(board.Victory, game.Player2, groupName);
                }
            }
            catch (Exception ex)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("Errore", ex.Message);
            }
        }
    }
}
