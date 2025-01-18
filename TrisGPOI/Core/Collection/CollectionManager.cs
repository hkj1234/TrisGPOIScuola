using TrisGPOI.Core.Collection.Interfaces;
using TrisGPOI.Database.Collection.Entities;

namespace TrisGPOI.Core.Collection
{
    public class CollectionManager : ICollectionManager
    {
        private readonly ICollectionRepository _collectionRepository;

        public CollectionManager(ICollectionRepository collectionRepository)
        {
            _collectionRepository = collectionRepository;
        }

        public async Task<List<DBCollection>> GetCollectionList()
        {
            return await _collectionRepository.GetCollectionList();
        }

        public async Task<List<DBRarity>> GetRarityList()
        {
            return await _collectionRepository.GetRarityList();
        }

        public async Task<DBCollection> GetCollection(string name)
        {
            return await _collectionRepository.GetCollection(name);
        }

        public async Task<DBRarity> GetRarity(string name)
        {
            return await _collectionRepository.GetRarity(name);
        }

        public async Task<bool> ValidateCollection(string name)
        {
            return await _collectionRepository.ValidateCollection(name);
        }

        public async Task<bool> ValidateRarity(string name)
        {
            return await _collectionRepository.ValidateRarity(name);
        }

        public async Task<DBCollection> GetCollection(int id)
        {
            return await _collectionRepository.GetCollection(id);
        }
    }
}
