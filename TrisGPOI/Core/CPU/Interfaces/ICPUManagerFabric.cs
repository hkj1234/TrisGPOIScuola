namespace TrisGPOI.Core.CPU.Interfaces
{
    public interface ICPUManagerFabric
    {
        ITypeCPUManagerFabric CreateTypeCPUFabric(string type);
    }
}
