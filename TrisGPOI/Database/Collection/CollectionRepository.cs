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
            { 'Id': 1, 'Name': 'null', 'Description': '', 'RarityID': 0 },
            { 'Id': 2, 'Name': 'Default', 'Description': '', 'RarityID': 0 },
            { 'Id': 3, 'Name': 'bambina', 'Description': '', 'RarityID': 2 },
            { 'Id': 4, 'Name': 'bambino', 'Description': '', 'RarityID': 2 },
            { 'Id': 5, 'Name': 'made_in_heaven', 'Description': '', 'RarityID': 2 },
            { 'Id': 6, 'Name': 'dragons_dream', 'Description': '', 'RarityID': 2 },
            { 'Id': 7, 'Name': 'paper_moon_king', 'Description': '', 'RarityID': 2 },
            { 'Id': 8, 'Name': 'arcade_machine', 'Description': '', 'RarityID': 2 },
            { 'Id': 9, 'Name': 'volpe', 'Description': '', 'RarityID': 2 },
            { 'Id': 10, 'Name': 'iggy', 'Description': '', 'RarityID': 1 },
            { 'Id': 11, 'Name': 'hermit_purple', 'Description': '', 'RarityID': 1 },
            { 'Id': 12, 'Name': 'gatto', 'Description': '', 'RarityID': 1 },
            { 'Id': 13, 'Name': 'man_in_the_mirror', 'Description': '', 'RarityID': 1 },
            { 'Id': 14, 'Name': 'the_world', 'Description': '', 'RarityID': 1 },
            { 'Id': 15, 'Name': 'white_snake', 'Description': '', 'RarityID': 1 },
            { 'Id': 16, 'Name': 'c_moon', 'Description': '', 'RarityID': 1 },
            { 'Id': 17, 'Name': 'ball_braker', 'Description': '', 'RarityID': 1 },
            { 'Id': 18, 'Name': 'cuore', 'Description': '', 'RarityID': 1 },
            { 'Id': 19, 'Name': 'gomma', 'Description': '', 'RarityID': 1 },
            { 'Id': 20, 'Name': 'uomo', 'Description': '', 'RarityID': 1 },
            { 'Id': 21, 'Name': 'donna', 'Description': '', 'RarityID': 1 },
            { 'Id': 22, 'Name': 'magicians_red', 'Description': '', 'RarityID': 3 },
            { 'Id': 23, 'Name': 'diamond_is_umbreakble', 'Description': '', 'RarityID': 3 },
            { 'Id': 24, 'Name': 'joker', 'Description': '', 'RarityID': 3 },
            { 'Id': 25, 'Name': 'heaven_doors', 'Description': '', 'RarityID': 3 },
            { 'Id': 26, 'Name': 'king_crimson', 'Description': '', 'RarityID': 3 },
            { 'Id': 27, 'Name': 'sfondo_arcobaleno', 'Description': '', 'RarityID': 3 },
            { 'Id': 28, 'Name': 'dio', 'Description': '', 'RarityID': 4 },
            { 'Id': 29, 'Name': 'jotaro', 'Description': '', 'RarityID': 4 },
            { 'Id': 30, 'Name': 'star_platinum', 'Description': '', 'RarityID': 4 },
            { 'Id': 31, 'Name': 'killer_queen', 'Description': '', 'RarityID': 4 },
            { 'Id': 32, 'Name': 'trofeo', 'Description': '', 'RarityID': 5 }
        ]";

        private readonly string CollectionPriceByRarity = @"
        [
            { 'Id': 1, 'RarityID': 1, 'Price': 30 }, //1 game
            { 'Id': 2, 'RarityID': 2, 'Price': 100 }, //3 games
            { 'Id': 3, 'RarityID': 3, 'Price': 300 }, //1 day
            { 'Id': 4, 'RarityID': 4, 'Price': 1000 }, //3 days
            { 'Id': 5, 'RarityID': 5, 'Price': 3000 } //10 days
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
        public async Task<DBCollection> GetCollection(int id)
        {
            List<DBCollection> collections = JsonConvert.DeserializeObject<List<DBCollection>>(CollectionJson);
            return collections.FirstOrDefault(x => x.Id == id);
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
        public async Task<List<DBCollection>> GetCollectionListByRarity(string rarityName)
        {
            List<DBCollection> collections = JsonConvert.DeserializeObject<List<DBCollection>>(CollectionJson);
            int rarityId = await GetRarityId(rarityName);
            return collections.Where(x => x.RarityID == rarityId).ToList();
        }
        public async Task<int> GetRarityId(string rarityName)
        {
            List<DBRarity> rarities = JsonConvert.DeserializeObject<List<DBRarity>>(RaritynJson);
            return rarities.FirstOrDefault(x => x.Name == rarityName).Id;
        }
        public async Task<int> GetRarityPrice(string rarityName)
        {
            List<DbRarityPrice> rarityPrices = JsonConvert.DeserializeObject<List<DbRarityPrice>>(CollectionPriceByRarity);
            int rarityId = await GetRarityId(rarityName);
            return rarityPrices.FirstOrDefault(x => x.RarityID == rarityId).Price;
        }
    }
}
