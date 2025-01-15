using TrisGPOI.Core.Collection.Interfaces;
using TrisGPOI.Database.Collection.Entities;

namespace TrisGPOI.Core.Collection
{
    public class CollectionInventoryManager : ICollectionInventoryManager
    {
        private readonly ICollectionInventoryRepository _collectionInventoryRepository;
        private readonly ICollectionManager _collectionManager;
        public CollectionInventoryManager(ICollectionInventoryRepository collectionInventoryRepository, ICollectionManager collectionManager)
        {
            _collectionInventoryRepository = collectionInventoryRepository;
            _collectionManager = collectionManager;
        }
        public async Task<List<DBCollectionInventory>> GetInventory(string userEmail)
        {
            return await _collectionInventoryRepository.GetInventory(userEmail);
        }
        public async Task addCollection(string userEmail, string collectionName, int quantity)
        {
            var collection = await _collectionManager.GetCollection(collectionName);
            await _collectionInventoryRepository.addCollection(userEmail, collection.Id, quantity);
        }
        public async Task removeCollection(string userEmail, string collectionName, int quantity)
        {
            var collection = await _collectionManager.GetCollection(collectionName);
            if (!await anyCollection(userEmail, collectionName))
            {
                throw new Exception("Collection not found");
            }
            await _collectionInventoryRepository.removeCollection(userEmail, collection.Id, quantity);
        }
        public async Task removeAllCollection(string userEmail, string collectionName)
        {
            var inventory = await GetInventory(userEmail);
            var collection = await _collectionManager.GetCollection(collectionName);
            var UserCollection = inventory.FirstOrDefault(i => i.CollectionID == collection.Id);
            if (UserCollection == null)
            {
                throw new Exception("Collection not found");
            }
            await _collectionInventoryRepository.removeCollection(userEmail, collection.Id, UserCollection.Quantity);
        }
        public async Task<bool> anyCollection(string userEmail, string collectionName)
        {
            var collection = await _collectionManager.GetCollection(collectionName);
            return await _collectionInventoryRepository.anyCollection(userEmail, collection.Id);
        }
    }
}
