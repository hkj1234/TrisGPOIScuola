using TrisGPOI.Core.Game.Exceptions;
using TrisGPOI.Core.Game.Interfaces;

namespace TrisGPOI.Core.Game
{
    public class TrisNormaleManager : ITrisManager
    {
        public string PlayMove(string board, int position, char simbol)
        {
            int maxPosition = board.Length;
            var temp = board.ToCharArray();
            if (position >= maxPosition || temp[position] != '-' || CheckWin(board) != '-')
            {
                throw new InvalidPlayerMoveException();
            }
            temp[position] = simbol;
            return new string(temp);
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

            if (IsEmpty(board))
            {
                return '0';
            }

            // Nessun vincitore
            return '-';
        }
        public bool IsEmptyPosition(string board, int position)
        {
            return board[position] == '-';
        }
        public bool IsEmpty(string board)
        {
            return ! board.Contains("-");
        }
        public string CreateEmptyBoard()
        {
            return new string('-', 9);
        }
        public List<int> GetValidPosition(string board)
        {
            return board.Select((c, i) => new { c, i })
                        .Where(x => IsEmptyPosition(board, x.i))
                        .Select(x => x.i)
                        .ToList();
        }
        public char GetPosition(string board, int position)
        {
            return board[position];
        }
    }
}
