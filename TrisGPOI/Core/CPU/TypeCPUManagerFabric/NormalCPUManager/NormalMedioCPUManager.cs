using TrisGPOI.Core.CPU.Interfaces;
using TrisGPOI.Core.Game.Interfaces;

namespace TrisGPOI.Core.CPU.TypeCPUManagerFabric.NormalCPUManager
{
    public class NormalMedioCPUManager : ICPUManager
    {
        private readonly ITrisManager _trisManager;
        public NormalMedioCPUManager(ITrisManager trisManager)
        {
            _trisManager = trisManager;
        }

        //previene solo la mossa migliore al momento
        public int GetCPUMove(string board)
        {
            char ai = '2';
            char giocatore = '1';

            // Controlla se l'IA può vincere in questa mossa
            for (int i = 0; i < 9; i++)
            {
                if (_trisManager.IsEmptyPosition(board, i))
                {
                    var tempBoard = _trisManager.PlayMove(board, i, ai);
                    if (_trisManager.CheckWin(tempBoard) == '2')
                    {
                        return i;
                    }
                }
            }

            // Controlla se l'avversario può vincere nella prossima mossa e bloccalo
            for (int i = 0; i < 9; i++)
            {
                if (_trisManager.IsEmptyPosition(board, i))
                {
                    var tempBoard = _trisManager.PlayMove(board, i, giocatore);
                    if (_trisManager.CheckWin(tempBoard) == '1')
                    {
                        return i;
                    }
                }
            }

            // Prendi la posizione centrale se disponibile
            if (_trisManager.IsEmptyPosition(board, 4))
            {
                return 4;
            }

            // Prendi una posizione angolare se disponibile
            int[] angoli = { 0, 2, 6, 8 };
            foreach (int angolo in angoli)
            {
                if (_trisManager.IsEmptyPosition(board, angolo))
                {
                    return angolo;
                }
            }

            // Prendi una posizione laterale se disponibile
            int[] lati = { 1, 3, 5, 7 };
            foreach (int lato in lati)
            {
                if (_trisManager.IsEmptyPosition(board, lato))
                {
                    return lato;
                }
            }

            // Se tutte le posizioni sono piene (non dovrebbe accadere in una partita corretta)
            return -1;
        }
    }
}
