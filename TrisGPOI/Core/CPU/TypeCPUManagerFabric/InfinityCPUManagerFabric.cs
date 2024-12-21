using TrisGPOI.Core.CPU.Exceptions;
using TrisGPOI.Core.CPU.Interfaces;
using TrisGPOI.Core.CPU.TypeCPUManagerFabric.InfinityCPUManager;
using TrisGPOI.Core.CPU.TypeCPUManagerFabric.NormalCPUManager;
using TrisGPOI.Core.Game;
using TrisGPOI.Core.Game.Interfaces;

namespace TrisGPOI.Core.CPU.TypeCPUManagerFabric
{
    public class InfinityCPUManagerFabric : ITypeCPUManagerFabric
    {
        private readonly ITrisManager _trisManager;
        public InfinityCPUManagerFabric(ITrisManager trisManager)
        {
            _trisManager = trisManager;
        }
        public ICPUManager CreateCPUManager(string difficulty)
        {
            if (difficulty == "Facile")
            {
                return new InfinityFacileCPUManager(_trisManager);
            }
            else if (difficulty == "Medio")
            {
                return new InfinityMedioCPUManager(_trisManager);
            }
            else if (difficulty == "Difficile")
            {
                return new InfinityDifficileCPUManager(_trisManager);
            }
            return null;
        }
    }
}
