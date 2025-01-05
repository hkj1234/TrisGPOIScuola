using TrisGPOI.Core.Game.Exceptions;
using TrisGPOI.Core.Game.Interfaces;

namespace TrisGPOI.Core.Game.TypeTrisManager
{
    public class TrisUltimateManager : ITrisManager
    {
        private readonly TrisNormaleManager _trisNormaleManager = new TrisNormaleManager();
        private readonly int _miniBoardLenght = 81;
        private readonly int _boardLenght = 92;
        public string PlayMove(string board, int position, char simbol)
        {
            //Aggiornare la posizione al board
            board = board.Substring(0, board.Length - 2) + position.ToString("D2");

            var temp = board.ToCharArray();
            checkValidPosition(board, position);
            
            temp[position] = simbol;
            
            return CheckWinFirstPartBoard(new string(temp));
        }
        public char CheckWin(string board)
        {
            // Controlla se la stringa ha una lunghezza valida
            if (board.Length != _boardLenght)
            {
                throw new ArgumentException("The grid string must contain exactly 92 characters.");
            }
            string subBoard = GetSecondPartBoard(board);
            return _trisNormaleManager.CheckWin(subBoard);
        }
        public bool IsEmptyPosition(string board, int position)
        {
            string subBoard = GetFirstPartBoard(board);
            return subBoard[position] == '-';
        }
        public bool IsEmpty(string board)
        {
            string subBoard = GetFirstPartBoard(board);
            return !subBoard.Contains("-");
        }
        public string CreateEmptyBoard()
        {
            return new string('-', _boardLenght);
        }
        public List<int> GetValidPosition(string board)
        {
            string subBoard = GetFirstPartBoard(board);
            return subBoard.Select((c, i) => new { c, i })
                        .Where(x => IsEmptyPosition(subBoard, x.i))
                        .Select(x => x.i)
                        .ToList();
        }
        public char GetPosition(string board, int position)
        {
            string subBoard = GetFirstPartBoard(board);
            return subBoard[position];
        }
        private string CheckWinFirstPartBoard(string board)
        {
            var temp = board.ToCharArray();
            for (int i = 0; i < 9; i++)
            {
                string subBoard = board.Substring(i * 9, 9);
                temp[_miniBoardLenght+i] = _trisNormaleManager.CheckWin(subBoard);
            }
            return new string(temp);
        }
        private string GetFirstPartBoard(string board)
        {
            return board.Substring(0, _miniBoardLenght);
        }
        private string GetSecondPartBoard(string board)
        {
            return board.Substring(_miniBoardLenght, _boardLenght - _miniBoardLenght);
        }
        private void checkValidPosition(string board, int position)
        {
            //se è la prima volta che si gioca
            if (board[board.Length - 1] == '-')
            {
                return;
            }

            bool invalid = false;
            string bigBoard = GetSecondPartBoard(board);

            int thisSubTris = position / 9;

            string lastPositionString = board.Substring(board.Length - 2);
            int lastPosition = int.Parse(lastPositionString);
            int lastSubTris = lastPosition / 9;

            //se il numero della posizione è maggiore di 81 o
            //la posizione è già occupata o
            //la partita è già vinta
            if (position >= _boardLenght || board[position] != '-' || CheckWin(board) != '-')
            {
                invalid = true;
            }

            //se la SubTris è già finita
            if (bigBoard[thisSubTris] != '-')
            {
                invalid = true;
            }

            //se la SubTris non è posibile giocare
            if (bigBoard[lastSubTris] == '-' && lastSubTris != thisSubTris)
            {
                invalid = true;
            }

            if (invalid)
            {
                throw new InvalidPlayerMoveException();
            }
        }
    }
}
