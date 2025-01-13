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
    }
}
