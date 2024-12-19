using TrisGPOI.Core.CPU.Interfaces;
using TrisGPOI.Core.Game.Interfaces;

namespace TrisGPOI.Core.CPU.TypeCPUManagerFabric.NormalCPUManager
{
    public class DifficileCPUManager : ICPUManager
    {
        private readonly ITrisManager _trisManager;
        public DifficileCPUManager(ITrisManager trisManager)
        {
            _trisManager = trisManager;
        }

        public int GetCPUMove(string board)
        {
            char[] griglia = board.ToCharArray();
            int migliorMossa = -1;
            int migliorValore = int.MinValue;
            char giocatore = '1';
            char ai = '2';

            for (int i = 0; i < 9; i++)
            {
                if (griglia[i] == '-')
                {
                    griglia[i] = ai;
                    int valoreMossa = Minimax(griglia, false, ai, giocatore, 0);
                    griglia[i] = '-';

                    if (valoreMossa > migliorValore)
                    {
                        migliorValore = valoreMossa;
                        migliorMossa = i;
                    }
                }
            }
            return migliorMossa;
        }

        public int Minimax(char[] griglia, bool isMax, char ai, char giocatore, int depth)
        {
            char risultato = _trisManager.CheckWin(new string(griglia));
            if (risultato == ai) return 10 - depth; // Vittoria AI
            if (risultato == giocatore) return depth - 10; // Vittoria Giocatore
            if (!griglia.Contains('-')) return 0; // Pareggio

            if (isMax)
            {
                int migliorValore = int.MinValue;
                for (int i = 0; i < 9; i++)
                {
                    if (griglia[i] == '-')
                    {
                        griglia[i] = ai;
                        migliorValore = Math.Max(migliorValore, Minimax(griglia, false, ai, giocatore, depth + 1));
                        griglia[i] = '-';
                    }
                }
                return migliorValore;
            }
            else
            {
                int migliorValore = int.MaxValue;
                for (int i = 0; i < 9; i++)
                {
                    if (griglia[i] == '-')
                    {
                        griglia[i] = giocatore;
                        migliorValore = Math.Min(migliorValore, Minimax(griglia, true, ai, giocatore, depth + 1));
                        griglia[i] = '-';
                    }
                }
                return migliorValore;
            }
        }

    }
}
