using TrisGPOI.Database.Collection.Entities;

namespace TrisGPOI.Core.Collection.Interfaces
{
    public interface ICollectionRepository
    {
        Task<List<DBCollection>> GetCollectionList();
        Task<List<DBRarity>> GetRarityList();
        Task<DBCollection> GetCollection(string name);
        Task<DBRarity> GetRarity(string name);
        Task<bool> ValidateCollection(string name);
        Task<bool> ValidateRarity(string name);
        Task<DBCollection> GetCollection(int id);
    }
}
