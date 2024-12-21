using TrisGPOI.Core.Game.Interfaces;

namespace TrisGPOI.Core.CPU
{
    public class MinMaxCPUMove
    {
        internal readonly ITrisManager _trisManager;
        public MinMaxCPUMove(ITrisManager trisManager)
        {
            _trisManager = trisManager;
        }
        public int GetCPUMove(string board, char giocatore, char ai, int limit = 10)
        {
            int migliorMossa = -1;
            int migliorValore = int.MinValue;
            var ValidPositions = _trisManager.GetValidPosition(board);

            foreach (int i in ValidPositions)
            {
                var tempBoard = _trisManager.PlayMove(board, i, ai);
                int valoreMossa = Minimax(tempBoard, false, ai, giocatore, 0, limit);

                if (valoreMossa > migliorValore)
                {
                    migliorValore = valoreMossa;
                    migliorMossa = i;
                }
            }
            return migliorMossa;
        }

        public int Minimax(string griglia, bool isMax, char ai, char giocatore, int depth, int limit = 10)
        {
            if (depth > limit)
            {
                return 0;
            }
            char risultato = _trisManager.CheckWin(griglia);
            if (risultato == ai) return limit - depth; // Vittoria AI
            if (risultato == giocatore) return depth - limit; // Vittoria Giocatore
            if (_trisManager.IsEmpty(griglia)) return 0; // Pareggio

            if (isMax)
            {
                int migliorValore = int.MinValue;
                foreach (int i in _trisManager.GetValidPosition(griglia))
                {
                    var tempBoard = _trisManager.PlayMove(griglia, i, ai);
                    migliorValore = Math.Max(migliorValore, Minimax(tempBoard, false, ai, giocatore, depth + 1, limit));
                }
                return migliorValore;
            }
            else
            {
                int migliorValore = int.MaxValue;
                foreach (int i in _trisManager.GetValidPosition(griglia))
                {
                    var tempBoard = _trisManager.PlayMove(griglia, i, giocatore);
                    migliorValore = Math.Min(migliorValore, Minimax(tempBoard, true, ai, giocatore, depth + 1, limit));
                }
                return migliorValore;
            }
        }
    }
}
