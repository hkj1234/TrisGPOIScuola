using TrisGPOI.Core.CPU.Interfaces;
using TrisGPOI.Core.Game.Interfaces;

namespace TrisGPOI.Core.CPU.TypeCPUManagerFabric.NormalCPUManager
{
    public class MedioCPUManager : ICPUManager
    {
        private readonly ITrisManager _trisManager;
        public MedioCPUManager(ITrisManager trisManager)
        {
            _trisManager = trisManager;
        }

        //previene solo la mossa migliore al momento
        public int GetCPUMove(string board)
        {
            char[] griglia = board.ToCharArray();
            char ai = '2';
            char giocatore = '1';

            // Controlla se l'IA può vincere in questa mossa
            for (int i = 0; i < 9; i++)
            {
                if (griglia[i] == '-')
                {
                    griglia[i] = ai;
                    if (_trisManager.CheckWin(board) == '2')
                    {
                        return i;
                    }
                    griglia[i] = '-';
                }
            }

            // Controlla se l'avversario può vincere nella prossima mossa e bloccalo
            for (int i = 0; i < 9; i++)
            {
                if (griglia[i] == '-')
                {
                    griglia[i] = giocatore;
                    if (_trisManager.CheckWin(board) == '1')
                    {
                        return i;
                    }
                    griglia[i] = '-';
                }
            }

            // Prendi la posizione centrale se disponibile
            if (griglia[4] == '-')
            {
                return 4;
            }

            // Prendi una posizione angolare se disponibile
            int[] angoli = { 0, 2, 6, 8 };
            foreach (int angolo in angoli)
            {
                if (griglia[angolo] == '-')
                {
                    return angolo;
                }
            }

            // Prendi una posizione laterale se disponibile
            int[] lati = { 1, 3, 5, 7 };
            foreach (int lato in lati)
            {
                if (griglia[lato] == '-')
                {
                    return lato;
                }
            }

            // Se tutte le posizioni sono piene (non dovrebbe accadere in una partita corretta)
            return -1;
        }
    }
}
