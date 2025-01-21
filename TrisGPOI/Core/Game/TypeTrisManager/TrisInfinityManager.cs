using TrisGPOI.Core.Game.Exceptions;
using TrisGPOI.Core.Game.Interfaces;

namespace TrisGPOI.Core.Game.TypeTrisManager
{
    public class TrisInfinityManager : ITrisManager
    {
        public string PlayMove(string board, int position, char simbol)
        {
            int maxPosition = 9;
            var temp = board.ToCharArray();
            if (position >= maxPosition || !IsEmptyPosition(board, position) || CheckWin(board) != '-')
            {
                throw new InvalidPlayerMoveException();
            }

            temp[position * 2] = simbol;
            temp[position * 2 + 1] = '0';

            for (int i = 0; i < maxPosition; i++)
            {
                if (!IsEmptyPosition(new string(temp), i))
                {
                    temp[i * 2 + 1]++;
                    if (temp[i * 2 + 1] > '6')
                    {
                        temp[i * 2] = '-';
                        temp[i * 2 + 1] = '-';
                    }
                }
            }

            return new string(temp);
        }
        public char CheckWin(string board)
        {
            // Controlla se la stringa ha una lunghezza valida
            if (board.Length != 18)
            {
                throw new ArgumentException("The grid string must contain exactly 18 characters.");
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
                char a = GetPosition(board, condition[0]);
                char b = GetPosition(board, condition[1]);
                char c = GetPosition(board, condition[2]);

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
            return GetPosition(board, position) == '-';
        }
        public string CreateEmptyBoard()
        {
            return new string('-', 18);
        }
        public bool IsEmpty(string board)
        {
            return false;
        }
        public List<int> GetValidPosition(string board)
        {
            return Enumerable.Range(0, 9)
                    .Where(i => IsEmptyPosition(board, i))
                    .ToList();
        }
        public char GetPosition(string board, int position)
        {
            return board[position * 2];
        }
    }
}
