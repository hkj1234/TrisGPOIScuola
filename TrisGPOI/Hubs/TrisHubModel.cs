﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TrisGPOI.Core.Game;
using TrisGPOI.Core.Game.Entities;
using TrisGPOI.Core.Game.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TrisGPOI.Hubs
{
    [Authorize]
    public class TrisHubModel : Hub
    {
        private readonly IGameManager _gameManager;
        private readonly string _type;
        private readonly IGameVictoryManager _gameVictoryManager;
        public TrisHubModel(IGameManager gameManager, string type, IGameVictoryManager gameVictoryManager)
        {
            _gameManager = gameManager;
            _type = type;
            _gameVictoryManager = gameVictoryManager;
        }
        public override async Task OnConnectedAsync()
        {
            var email = Context.User?.Identity?.Name;
            try
            {
                await _gameManager.JoinGame(email, _type);
            }
            catch(Exception e)
            {
            }

            try
            {
                var game = await _gameManager.SearchPlayerPlayingOrWaitingGameAsync(email);
                string connectionId = Context.ConnectionId;

                if (game != null && game.GameType == _type && ( game.Player2 == null || game.Player2.Contains('@')))
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

                await CheckComunicationWinning(board, email, groupName);
            }
            catch (Exception ex)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("Errore", ex.Message);
            }       
        }

        private async Task CheckComunicationWinning(BoardInfo info, string email, string groupName)
        {
            char value = info.Victory;
            if (value == '0')
            {
                await Clients.Group(groupName).SendAsync("Winning", '0');
                await _gameVictoryManager.GameFinished(info.Player1, info.Player2, "0", _type);
            }
            else if (value != '-')
            {
                await Clients.Group(groupName).SendAsync("Winning", email);
                await _gameVictoryManager.GameFinished(info.Player1, info.Player2, email, _type);
            }
        }
    }
}





