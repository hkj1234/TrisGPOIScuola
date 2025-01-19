using TrisGPOI.Database.Collection.Entities;

namespace TrisGPOI.Core.Collection.Interfaces
{
    public interface ICollectionManager
    {
        Task<List<DBCollection>> GetCollectionList();
        Task<List<DBRarity>> GetRarityList();
        Task<DBCollection> GetCollection(string name);
        Task<DBRarity> GetRarity(string name);
        Task<bool> ValidateCollection(string name);
        Task<bool> ValidateRarity(string name);
        Task<DBCollection> GetCollection(int id);
        Task<List<DBCollection>> GetCollectionListByRarity(string rarityName);
        Task<int> GetRarityPrice(string rarityName);
    }
}
