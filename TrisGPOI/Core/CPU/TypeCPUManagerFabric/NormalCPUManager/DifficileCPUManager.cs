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
                if (griglia[i] == ' ')
                {
                    griglia[i] = ai;
                    int valoreMossa = Minimax(griglia, false, ai, giocatore);
                    griglia[i] = ' ';
                    if (valoreMossa > migliorValore)
                    {
                        migliorValore = valoreMossa;
                        migliorMossa = i;
                    }
                }
            }
            return migliorMossa;
        }

        public int Minimax(char[] griglia, bool isMax, char ai, char giocatore)
        {
            char punteggioChar = _trisManager.CheckWin(new string(griglia));
            int punteggio = 0;
            if (punteggioChar == '-')
            {
                punteggio = 0;
            }
            else if(punteggioChar == '2')
            {
                punteggio = 1;
            }
            else if (punteggioChar == '1')
            {
                punteggio = -1;
            }

            if (punteggio != 0 || !griglia.Contains(' '))
            {
                return punteggio;
            }

            if (isMax)
            {
                int migliorValore = int.MinValue;
                for (int i = 0; i < 9; i++)
                {
                    if (griglia[i] == ' ')
                    {
                        griglia[i] = giocatore;
                        migliorValore = Math.Max(migliorValore, Minimax(griglia, false, ai, giocatore));
                        griglia[i] = ' ';
                    }
                }
                return migliorValore;
            }
            else
            {
                int migliorValore = int.MaxValue;
                for (int i = 0; i < 9; i++)
                {
                    if (griglia[i] == ' ')
                    {
                        griglia[i] = giocatore;
                        migliorValore = Math.Min(migliorValore, Minimax(griglia, true, ai, giocatore));
                        griglia[i] = ' ';
                    }
                }
                return migliorValore;
            }
        }
    }
}
