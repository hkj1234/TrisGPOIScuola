using TrisGPOI.Core.CPU.Interfaces;
using TrisGPOI.Core.Game.Interfaces;

namespace TrisGPOI.Core.CPU.TypeCPUManagerFabric.InfinityCPUManager
{
    public class InfinityDifficileCPUManager : MinMaxCPUMove, ICPUManager
    {
        public InfinityDifficileCPUManager(ITrisManager trisManager) : base(trisManager) { }

        public int GetCPUMove(string board)
        {
            return base.GetCPUMove(board, '1', '2', 9);
        }
    }
}
