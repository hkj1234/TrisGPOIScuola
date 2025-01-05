using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Core.Game.TypeTrisManager;

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
            else if (type == "Ultimate")
            {
                return new TrisUltimateManager();
            }
            return null;
        }
    }
}
