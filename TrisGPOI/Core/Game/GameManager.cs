﻿using System.Reflection.Metadata;
using TrisGPOI.Core.CPU;
using TrisGPOI.Core.CPU.Interfaces;
using TrisGPOI.Core.Game.Entities;
using TrisGPOI.Core.Game.Exceptions;
using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Database.Game.Entities;

namespace TrisGPOI.Core.Game
{
    public class GameManager : IGameManager
    {
        private readonly IGameRepository _gameRepository;
        private readonly ITrisManagerFabric _trisManagerFabric;
        private readonly ICPUManagerFabric _cPUManagerFabric;
        public GameManager(IGameRepository gameRepository, ITrisManagerFabric trisManagerFabric, ICPUManagerFabric cPUManagerFabric)
        {
            _gameRepository = gameRepository;
            _trisManagerFabric = trisManagerFabric;
            _cPUManagerFabric = cPUManagerFabric;
        }

        public async Task<BoardInfo> PlayMove(string playerEmail, int position)
        {
            //vedere se i dati inseriti sono validi
            DBGame? game = await _gameRepository.SearchPlayerPlayingGame(playerEmail);
            if (game == null)
            {
                throw new NoGamePlayingException();
            }
            int maxPosition = game.Board.Length;
            string currentPlayer = game.CurrentPlayer;
            if (currentPlayer != playerEmail)
            {
                throw new InvalidPlayerMoveException();
            }

            //giocare la mossa
            ITrisManager _trisManager = _trisManagerFabric.CreateTrisManager(game.GameType);
            if (_trisManager == null)
            {
                throw new ServerDataErrorException();
            }
            char simbol = game.CurrentPlayer == game.Player1 ? '1' : '2';
            string board = _trisManager.PlayMove(game.Board, position, simbol);
            await _gameRepository.UpdateBoard(playerEmail, board);

            //vediamo se qualcuno ha vinto
            char ris = _trisManager.CheckWin(board);
            if (ris != '-')
            {
                await _gameRepository.GameFinished(game.Id);
            }

            //restituire il risultato del board
            return new BoardInfo { 
                Board = board, 
                Victory = ris,
                Player1 = game.Player1,
                Player2 = game.Player2,
                CurrentPlayer = currentPlayer == game.Player1 ? game.Player2 : game.Player1,
                LastMoveTime = DateTime.UtcNow,
                GameType = game.GameType,
            };
        }

        public async Task<BoardInfo> CPUPlayMove(string playerEmail)
        {
            DBGame? game = await _gameRepository.SearchPlayerPlayingGame(playerEmail);
            if (game == null)
            {
                throw new NoGamePlayingException();
            }

            //creare variabili
            var TypeCPUManagerFabric = _cPUManagerFabric.CreateTypeCPUFabric(game.GameType);
            if (TypeCPUManagerFabric == null)
            {
                throw new ServerDataErrorException();
            }
            var CPUManager = TypeCPUManagerFabric.CreateCPUManager(game.Player2);
            if (CPUManager == null)
            {
                throw new ServerDataErrorException();
            }
            ITrisManager _trisManager = _trisManagerFabric.CreateTrisManager(game.GameType);

            //gioca AI
            int position = CPUManager.GetCPUMove(game.Board);
            char simbol = '2';
            var board = _trisManager.PlayMove(game.Board, position, simbol);
            await _gameRepository.UpdateBoard(playerEmail, board);

            //vediamo se qualcuno ha vinto
            char ris = _trisManager.CheckWin(board);
            if (ris != '-')
            {
                await _gameRepository.GameFinished(game.Id);
            }

            //restituire il risultato del board
            return new BoardInfo
            {
                Board = board,
                Victory = ris,
                Player1 = game.Player1,
                Player2 = game.Player2,
                CurrentPlayer = game.CurrentPlayer == game.Player1 ? game.Player2 : game.Player1,
                LastMoveTime = DateTime.UtcNow,
                GameType = game.GameType,
            };
        }

        public async Task<DBGame?> SearchPlayerPlayingOrWaitingGameAsync(string playerEmail)
        {
            DBGame? actualGame = await _gameRepository.SearchPlayerPlayingOrWaitingGame(playerEmail);
            return actualGame;
        }

        public async Task<DBGame?> SearchPlayerPlayingGameAsync(string playerEmail)
        {
            DBGame? actualGame = await _gameRepository.SearchPlayerPlayingGame(playerEmail);
            return actualGame;
        }

        public async Task JoinGame(string playerEmail, string gameType)
        {
            string[] possibleType =
            {
                "Normal",
            };
            bool possible = false;
            foreach (var type in possibleType)
            {
                if (gameType == type)
                {
                    possible = true;
                }
            }

            if (!possible)
            {
                throw new InvalidGameTypeException();
            }

            if (await SearchPlayerPlayingOrWaitingGameAsync(playerEmail) != null)
            {
                throw new ExistGameException();
            }
            
            bool ris = await _gameRepository.JoinSomeGame(gameType, playerEmail);
            if (!ris)
            {
                await _gameRepository.StartJoinGame(gameType, playerEmail);
            }
        }

        public async Task PlayWithCPU(string playerEmail, string type, string difficult)
        {
            string[] possibleType =
            {
                "Normal",
            };
            bool possible = false;
            foreach (var temp in possibleType)
            {
                if (type == temp)
                {
                    possible = true;
                }
            }

            if (!possible)
            {
                throw new InvalidGameTypeException();
            }

            if (await SearchPlayerPlayingOrWaitingGameAsync(playerEmail) != null)
            {
                throw new ExistGameException();
            }

            await _gameRepository.StartJoinCPUGame(type, playerEmail, difficult);
        }

        public async Task CancelSearchGame(string email)
        {
            await _gameRepository.CancelSearchGame(email);
        }
    }
}
