using Newtonsoft.Json;
using TrisGPOI.Core.Collection.Interfaces;
using TrisGPOI.Database.Collection.Entities;

namespace TrisGPOI.Database.Collection
{
    public class CollectionRepository : ICollectionRepository
    {
        private readonly string RaritynJson = @"
        [
            { 'Id': 0, 'Name': 'null' },
            { 'Id': 1, 'Name': 'common' },
            { 'Id': 2, 'Name': 'uncommon' },
            { 'Id': 3, 'Name': 'rare' },
            { 'Id': 4, 'Name': 'epic' },
            { 'Id': 5, 'Name': 'legendary' }
        ]";

        private readonly string CollectionJson = @"
        [
            {
                'Id': 1,
                'Name': '',
                'Description': '',
                'RarityID': 0
            },
            {
                'Id': 2,
                'Name': 'Default',
                'Description': '',
                'RarityID': 0
            }
        ]";

        public async Task<List<DBCollection>> GetCollectionList()
        {
            List<DBCollection> collections = JsonConvert.DeserializeObject<List<DBCollection>>(CollectionJson);
            return collections;
        }
        public async Task<List<DBRarity>> GetRarityList()
        {
            List<DBRarity> rarities = JsonConvert.DeserializeObject<List<DBRarity>>(RaritynJson);
            return rarities;
        }
        public async Task<DBCollection> GetCollection(string name)
        {
            List<DBCollection> collections = JsonConvert.DeserializeObject<List<DBCollection>>(CollectionJson);
            return collections.FirstOrDefault(x => x.Name == name);
        }
        public async Task<DBRarity> GetRarity(string name)
        {
            List<DBRarity> rarities = JsonConvert.DeserializeObject<List<DBRarity>>(RaritynJson);
            return rarities.FirstOrDefault(x => x.Name == name);
        }
        public async Task<bool> ValidateCollection(string name)
        {
            List<DBCollection> collections = JsonConvert.DeserializeObject<List<DBCollection>>(CollectionJson);
            return collections.Any(x => x.Name == name);
        }
        public async Task<bool> ValidateRarity(string name)
        {
            List<DBRarity> rarities = JsonConvert.DeserializeObject<List<DBRarity>>(RaritynJson);
            return rarities.Any(x => x.Name == name);
        }
    }
}
