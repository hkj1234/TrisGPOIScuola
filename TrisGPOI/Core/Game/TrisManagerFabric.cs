using TrisGPOI.Core.Game.Interfaces;

namespace TrisGPOI.Core.Game
{
    public class TrisManagerFabric : ITrisManagerFabric
    {
        public ITrisManager CreateTrisManager(string type)
        {
            if (type == "Normal")
            {
                return new TrisNormaleManager();
            }
            else if (type == "Infinity")
            {
                return new TrisInfinityManager();
            }
            return null;
        }
    }
}
