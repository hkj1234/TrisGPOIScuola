using TrisGPOI.Core.CPU.Interfaces;
using TrisGPOI.Core.Game.Interfaces;

namespace TrisGPOI.Core.CPU.TypeCPUManagerFabric.InfinityCPUManager
{
    public class InfinityMedioCPUManager : MinMaxCPUMove, ICPUManager
    {
        public InfinityMedioCPUManager(ITrisManager trisManager) : base(trisManager) { }

        public int GetCPUMove(string board)
        {
            return base.GetCPUMove(board, '1', '2', 6);
        }
    }
}
