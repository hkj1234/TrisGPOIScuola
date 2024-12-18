using TrisGPOI.Core.CPU.Exceptions;
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
                return new FacileCPUManager(_trisManager);
            }
            else if (difficulty == "Medio")
            {
                return new MedioCPUManager(_trisManager);
            }
            else if (difficulty == "Difficile")
            {
                return new DifficileCPUManager(_trisManager);
            }
            return null;
        }
    }
}
