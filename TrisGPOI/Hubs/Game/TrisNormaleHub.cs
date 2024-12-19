using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TrisGPOI.Core.Game;
using TrisGPOI.Core.Game.Entities;
using TrisGPOI.Core.Game.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TrisGPOI.Hubs.Game
{
    [Authorize]
    public class TrisNormaleHub : Hub
    {
        private readonly IGameManager _gameManager;
        public TrisNormaleHub(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }
        public override async Task OnConnectedAsync()
        {
            var email = Context.User?.Identity?.Name;
            try
            {
                await _gameManager.JoinGame(email, "Normal");
            }
            catch(Exception e)
            {
            }

            try
            {
                var game = await _gameManager.SearchPlayerPlayingOrWaitingGameAsync(email);
                string connectionId = Context.ConnectionId;

                if (game != null && game.GameType == "Normal" && ( game.Player2 == null || game.Player2.Contains('@')))
                {
                    string groupName = game.Id.ToString();
                    // Aggiungi la nuova connessione
                    await Groups.AddToGroupAsync(connectionId, groupName);
                    await base.OnConnectedAsync();
                    await Clients.Group(groupName).SendAsync("Connection", email);
                    await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMove", arg1: game);
                }
                else
                {
                    Context.Abort();
                }
            }
            catch (Exception ex)
            {
                Context.Abort();
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var email = Context.User?.Identity?.Name;
                var game = await _gameManager.SearchPlayerPlayingOrWaitingGameAsync(email);
                await _gameManager.CancelSearchGame(email);
                if (game != null)
                {
                    string groupName = game.Id.ToString();
                    await Clients.Group(groupName).SendAsync("Disconnection", email);
                }
                await base.OnDisconnectedAsync(exception);
            }
            catch (Exception ex)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("Errore", ex.Message);
            }
        }
        public async Task SendMove(int position)
        {
            try
            {
                var email = Context.User?.Identity?.Name;
                var game = await _gameManager.SearchPlayerPlayingGameAsync(email);
                var board = await _gameManager.PlayMove(email, position);
                string groupName = game.Id.ToString();
                // Invia la mossa a tutti i client
                await Clients.Group(groupName).SendAsync("ReceiveMove", board);

                await Task.Delay(10);

                await CheckComunicationWinning(board.Victory, email, groupName);
            }
            catch (Exception ex)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("Errore", ex.Message);
            }       
        }

        private async Task CheckComunicationWinning(char value, string email, string groupName)
        {
            if (value == '0')
            {
                await Clients.Group(groupName).SendAsync("Winning", '0');
            }
            else if (value != '-')
            {
                await Clients.Group(groupName).SendAsync("Winning", email);
            }
        }
    }
}





