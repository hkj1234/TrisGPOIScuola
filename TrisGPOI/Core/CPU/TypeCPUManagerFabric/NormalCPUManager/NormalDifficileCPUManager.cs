﻿using TrisGPOI.Core.CPU.Interfaces;
using TrisGPOI.Core.Game.Interfaces;

namespace TrisGPOI.Core.CPU.TypeCPUManagerFabric.NormalCPUManager
{
    public class NormalDifficileCPUManager : MinMaxCPUMove, ICPUManager
    {
        public NormalDifficileCPUManager(ITrisManager trisManager) : base(trisManager) { }

        public int GetCPUMove(string board)
        {
            return base.GetCPUMove(board, '1', '2');
        }
    }
}
