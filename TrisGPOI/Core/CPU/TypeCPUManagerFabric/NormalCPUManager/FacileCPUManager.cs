﻿using TrisGPOI.Core.CPU.Interfaces;
using TrisGPOI.Core.Game.Interfaces;

namespace TrisGPOI.Core.CPU.TypeCPUManagerFabric.NormalCPUManager
{
    public class FacileCPUManager : ICPUManager
    {
        private readonly ITrisManager _trisManager;
        public FacileCPUManager(ITrisManager trisManager)
        {
            _trisManager = trisManager;
        }
        //facile, fa ramdomicamente le mosse
        public int GetCPUMove(string board)
        {
            Random random = new Random();
            var mossePossibili = _trisManager.GetValidPosition(board);
            return mossePossibili[random.Next(mossePossibili.Count)];
        }
    }
}
