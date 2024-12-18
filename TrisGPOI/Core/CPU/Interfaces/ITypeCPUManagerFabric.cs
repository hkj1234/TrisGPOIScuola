namespace TrisGPOI.Core.CPU.Interfaces
{
    public interface ITypeCPUManagerFabric
    {
        ICPUManager CreateCPUManager(string difficulty);
    }
}
