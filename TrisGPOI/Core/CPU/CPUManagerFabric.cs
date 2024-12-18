using TrisGPOI.Core.CPU.Interfaces;
using TrisGPOI.Core.CPU.TypeCPUManagerFabric;
using TrisGPOI.Core.Game.Interfaces;

namespace TrisGPOI.Core.CPU
{
    public class CPUManagerFabric : ICPUManagerFabric
    {
        private readonly ITrisManagerFabric _trisManagerFabric;
        public CPUManagerFabric(ITrisManagerFabric trisManagerFabric)
        {
            _trisManagerFabric = trisManagerFabric;
        }
        public ITypeCPUManagerFabric CreateTypeCPUFabric(string type)
        {
            if (type == "Normal")
            {
                return new NormalCPUManagerFabric(_trisManagerFabric.CreateTrisManager(type));
            }
            return null;
        }
    }
}
