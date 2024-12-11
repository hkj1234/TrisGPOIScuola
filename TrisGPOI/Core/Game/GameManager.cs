using TrisGPOI.Core.Game.Entities;
using TrisGPOI.Core.Game.Exceptions;
using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Database.Game.Entities;

namespace TrisGPOI.Core.Game
{
    public class GameManager : IGameManager
    {
        private readonly IGameRepository _gameRepository;
        public GameManager(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<BoardInfo> PlayMove(string playerEmail, int position)
        {
            DBGame? game = await _gameRepository.SearchPlayerPlayingGame(playerEmail);
            if (game == null)
            {
                throw new NoGamePlayingException();
            }
            int maxPosition = game.Board.Length;
            string currentPlayer = game.CurrentPlayer;
            if (position >= maxPosition || currentPlayer != playerEmail || game.Board[position] != '-')
            {
                throw new InvalidPlayerMoveException();
            }

            string board = await _gameRepository.PlayMove(playerEmail, position);

            char ris = CheckWin(board);
            if (ris != '-')
            {
                await _gameRepository.GameFinished(game.Id);
            }
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

        public async Task<DBGame?> SearchPlayerPlayingOrWaitingGameAsync(string playerEmail)
        {
            DBGame? actualGame = await _gameRepository.SearchPlayerPlayingOrWaitingGame(playerEmail);
            return actualGame;
        }

        public async Task JoinGame(string playerEmail, string gameType)
        {
            if (gameType != "Normal")
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

        public async Task CancelSearchGame(string email)
        {
            await _gameRepository.CancelSearchGame(email);
        }

        public char CheckWin(string board)
        {
            // Controlla se la stringa ha una lunghezza valida
            if (board.Length != 9)
            {
                throw new ArgumentException("The grid string must contain exactly 9 characters.");
            }

            // Definizione delle combinazioni vincenti (indici nella stringa)
            int[][] winConditions = new int[][]
            {
                new int[] { 0, 1, 2 }, // Prima riga
                new int[] { 3, 4, 5 }, // Seconda riga
                new int[] { 6, 7, 8 }, // Terza riga
                new int[] { 0, 3, 6 }, // Prima colonna
                new int[] { 1, 4, 7 }, // Seconda colonna
                new int[] { 2, 5, 8 }, // Terza colonna
                new int[] { 0, 4, 8 }, // Diagonale principale
                new int[] { 2, 4, 6 }  // Diagonale inversa
            };

            // Verifica ogni combinazione vincente
            foreach (var condition in winConditions)
            {
                char a = board[condition[0]];
                char b = board[condition[1]];
                char c = board[condition[2]];

                if (a != '-' && a == b && b == c)
                {
                    return a; // Ritorna il carattere del vincitore (es. '1', '2')
                }
            }

            // Nessun vincitore
            return '-';
        }
    }
}
