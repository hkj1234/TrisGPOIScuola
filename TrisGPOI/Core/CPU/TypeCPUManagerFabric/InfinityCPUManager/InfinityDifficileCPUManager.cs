using TrisGPOI.Core.CPU.Interfaces;
using TrisGPOI.Core.Game.Interfaces;

namespace TrisGPOI.Core.CPU.TypeCPUManagerFabric.InfinityCPUManager
{
    public class InfinityDifficileCPUManager : MinMaxCPUMove, ICPUManager
    {
        public InfinityDifficileCPUManager(ITrisManager trisManager) : base(trisManager) { }

        public int GetCPUMove(string board)
        {
            var ValidPositions = _trisManager.GetValidPosition(board);
            if (ValidPositions.Count() >= 7)
            {
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
            }
            return base.GetCPUMove(board, '1', '2', 10);
        }
    }
}
