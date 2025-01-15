using TrisGPOI.Database.Collection.Entities;

namespace TrisGPOI.Core.Collection.Interfaces
{
    public interface ICollectionInventoryManager
    {
        Task<List<DBCollectionInventory>> GetInventory(string userEmail);
        Task addCollection(string userEmail, string collectionName, int quantity);
        Task removeCollection(string userEmail, string collectionName, int quantity);
        Task removeAllCollection(string userEmail, string collectionName);
        Task<bool> anyCollection(string userEmail, string collectionName);
    }
}
