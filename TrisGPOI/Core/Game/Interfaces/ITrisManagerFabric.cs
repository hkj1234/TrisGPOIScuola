namespace TrisGPOI.Core.Game.Interfaces
{
    public interface ITrisManagerFabric
    {
        public ITrisManager CreateTrisManager(string type);
    }
}
