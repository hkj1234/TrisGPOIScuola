using TrisGPOI.Database.Collection.Entities;

namespace TrisGPOI.Core.Collection.Interfaces
{
    public interface ICollectionInventoryRepository
    {
        Task<List<DBCollectionInventory>> GetInventory(string userEmail);
        Task addCollection(string userEmail, int collectionId, int quantity);
        Task removeCollection(string userEmail, int collectionId, int quantity);
        Task<bool> anyCollection(string userEmail, int collectionId);
    }
}
