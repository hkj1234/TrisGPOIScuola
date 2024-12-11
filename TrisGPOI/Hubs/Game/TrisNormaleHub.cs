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
            
                if (game != null)
                {
                    string groupName = game.Id.ToString();
                    // Aggiungi la nuova connessione
                    await Groups.AddToGroupAsync(connectionId, groupName);
                    await base.OnConnectedAsync();
                    await Clients.Group(groupName).SendAsync("Connection", email);
                }
                else
                {
                    Context.Abort();
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
                await _gameManager.CancelSearchGame(email);
                await base.OnDisconnectedAsync(exception);

                var game = await _gameManager.SearchPlayerPlayingOrWaitingGameAsync(email);
                string groupName = game.Id.ToString();
                await Clients.Group(groupName).SendAsync("Disconnection", email);
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
                var board = await _gameManager.PlayMove(email, position);
                var game = await _gameManager.SearchPlayerPlayingOrWaitingGameAsync(email);
                string groupName = game.Id.ToString();
                // Invia la mossa a tutti i client
                await Clients.Group(groupName).SendAsync("ReceiveMove", board);
            }
            catch (Exception ex)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("Errore", ex.Message);
            }       
        }
    }
}





