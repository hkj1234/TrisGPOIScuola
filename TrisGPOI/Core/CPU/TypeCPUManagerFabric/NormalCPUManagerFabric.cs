﻿using TrisGPOI.Core.CPU.Exceptions;
using TrisGPOI.Core.CPU.Interfaces;
using TrisGPOI.Core.CPU.TypeCPUManagerFabric.NormalCPUManager;
using TrisGPOI.Core.Game;
using TrisGPOI.Core.Game.Interfaces;

namespace TrisGPOI.Core.CPU.TypeCPUManagerFabric
{
    public class NormalCPUManagerFabric : ITypeCPUManagerFabric
    {
        private readonly ITrisManager _trisManager;
        public NormalCPUManagerFabric(ITrisManager trisManager)
        {
            _trisManager = trisManager;
        }
        public ICPUManager CreateCPUManager(string difficulty)
        {
            if (difficulty == "Facile")
            {
                return new NormalFacileCPUManager(_trisManager);
            }
            else if (difficulty == "Medio")
            {
                return new NormalMedioCPUManager(_trisManager);
            }
            else if (difficulty == "Difficile")
            {
                return new NormalDifficileCPUManager(_trisManager);
            }
            return null;
        }
    }
}
