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
using TrisGPOI.Core.User.Interfaces;
using TrisGPOI.Hubs.TrisGameHub.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TrisGPOI.Hubs.TrisGameHub
{
    [Authorize]
    public class TrisHubModel : Hub
    {
        internal readonly IGameManager _gameManager;
        internal readonly string _type;
        internal static Dictionary<string, Timer> userTimers = new Dictionary<string, Timer>();
        private readonly IGameVictoryManager _gameVictoryManager;
        internal readonly IUserManager _userManager;
        internal async Task SimpleConectionAsync()
        {
            await base.OnConnectedAsync();
        }
        public TrisHubModel(IGameManager gameManager, string type, IGameVictoryManager gameVictoryManager, IUserManager userManager)
        {
            _gameManager = gameManager;
            _type = type;
            _gameVictoryManager = gameVictoryManager;
            _userManager = userManager;
        }
        public override async Task OnConnectedAsync()
        {
            
            await base.OnConnectedAsync();
        }

        public virtual async Task Spectator(string email)
        {
            var MyEmail = Context.User?.Identity?.Name;
            try
            {
                string connectionId = Context.ConnectionId;
                var game = await _gameManager.SearchPlayerPlayingOrWaitingGameAsync(email);
                if (game != null && game.GameType == _type)
                {
                    //ricevere la notifica
                    string groupName = game.Id.ToString();
                    await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMove", game);
                    await Groups.AddToGroupAsync(connectionId, groupName);
                }
            }
            catch (Exception ex)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("Errore", ex.Message);
            }
        }

        public virtual async Task Play()
        {
            var email = Context.User?.Identity?.Name;
            try
            {
                await _gameManager.JoinGame(email, _type);
                await _userManager.ChangeUserStatus(email, "Playing");
            }
            catch (Exception e)
            {
            }

            try
            {
                var game = await _gameManager.SearchPlayerPlayingOrWaitingGameAsync(email);
                string connectionId = Context.ConnectionId;

                if (game != null && game.GameType == _type && (game.Player2 == null || game.Player2.Contains('@')))
                {
                    string groupName = game.Id.ToString();
                    // Aggiungi la nuova connessione
                    await Groups.AddToGroupAsync(connectionId, groupName);
                    await base.OnConnectedAsync();
                    await Clients.Group(groupName).SendAsync("Connection", email);
                    await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMove", arg1: game);
                    updateTimer(groupName, game.LastMoveTime);
                }
            }
            catch (Exception ex)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("Errore", ex.Message);
                await _userManager.ChangeUserStatus(email, "Offline");
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var email = Context.User?.Identity?.Name;
                await _gameManager.CancelSearchGame(email);
                var game = await _gameManager.SearchPlayerPlayingOrWaitingGameAsync(email);
                if (game != null)
                {
                    string groupName = game.Id.ToString();
                    await Clients.Group(groupName).SendAsync("Disconnection", email);
                }
                else
                {
                    await _userManager.ChangeUserStatus(email, "Offline");
                }
                await base.OnDisconnectedAsync(exception);
            }
            catch (Exception ex)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("Errore", ex.Message);
            }
        }
        public virtual async Task SendMove(int position)
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

                if (board.Victory == '-')
                {
                    updateTimer(groupName, board.LastMoveTime);
                }
            }
            catch (Exception ex)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("Errore", ex.Message);
            }
        }
        public async Task Abandon()
        {
            try
            {
                var email = Context.User?.Identity?.Name;
                var game = await _gameManager.SearchPlayerPlayingGameAsync(email);
                await _gameManager.GameAbandon(email);
                string groupName = game.Id.ToString();
                // Invia la mossa a tutti i client
                string winner = game.Player1 == email ? game.Player2 : game.Player1;
                await Clients.Group(groupName).SendAsync("Winning", winner);
                await _gameVictoryManager.GameFinished(game.Player1, game.Player2, winner, _type);
            }
            catch (Exception ex)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("Errore", ex.Message);
            }
        }

        internal async Task CheckComunicationWinning(BoardInfo info, string email, string groupName)
        {
            char value = info.Victory;
            bool finished = false;
            if (value == '0')
            {
                await Clients.Group(groupName).SendAsync("Winning", '0');
                await _gameVictoryManager.GameFinished(info.Player1, info.Player2, "0", _type);
                finished = true;
            }
            else if (value != '-')
            {
                await Clients.Group(groupName).SendAsync("Winning", email);
                await _gameVictoryManager.GameFinished(info.Player1, info.Player2, email, _type);
                finished = true;
            }
            if (finished)
            {
                if (userTimers.ContainsKey(groupName))
                {
                    userTimers[groupName].Change(Timeout.Infinite, Timeout.Infinite);
                    userTimers[groupName].Dispose();
                }
                await _userManager.ChangeUserStatus(info.Player1, "Offline");
                if (info.Player2.Contains('@'))
                    await _userManager.ChangeUserStatus(info.Player2, "Offline");
            }
        }

        internal void updateTimer(string groupName, DateTime time)
        {
            if (userTimers.ContainsKey(groupName))
            {
                userTimers[groupName].Change(Timeout.Infinite, Timeout.Infinite);
                userTimers[groupName].Dispose();
            }

            // Calcola il tempo rimanente fino al momento specificato
            TimeSpan timeRemaining = time - DateTime.UtcNow;

            // Crea un nuovo timer per l'utente
            Timer timer = new Timer(TimeOut, new TimerStateForTimeOut
            {
                Clients = Clients,
                groupName = groupName,
            },
            timeRemaining, Timeout.InfiniteTimeSpan);
            userTimers[groupName] = timer;
        }

        private async void TimeOut(object state)
        {
            TimerStateForTimeOut info = (TimerStateForTimeOut)state;
            string groupName = info.groupName;

            var game = await _gameManager.SearchGameWithId(groupName);
            string winner = game.Player1 == game.CurrentPlayer ? game.Player2 : game.Player1;

            await info.Clients.Group(groupName).SendAsync("Winning", winner);
            await _gameVictoryManager.GameFinished(game.Player1, game.Player2, winner, _type);

            await _gameManager.GameAbandon(game.CurrentPlayer);
        }
    }
}





