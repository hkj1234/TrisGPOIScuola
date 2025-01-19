using TrisGPOI.Core.Collection.Interfaces;
using TrisGPOI.Core.Money.Interfaces;
using TrisGPOI.Core.Shop.Entities;
using TrisGPOI.Core.Shop.Interfaces;
using TrisGPOI.Database.Collection.Entities;

namespace TrisGPOI.Core.Shop
{
    public class ShopManager : IShopManager
    {
        private readonly IShopRepository _shopRepository;
        private readonly ICollectionManager _collectionManager;
        private readonly IMoneyXOManager _moneyManager;   
        public ShopManager(IShopRepository shopRepository, ICollectionManager collectionManager, IMoneyXOManager moneyManager)
        {
            _shopRepository = shopRepository;
            _collectionManager = collectionManager;
            _moneyManager = moneyManager;
        }
        public async Task<List<ShopInfo>> GetShops(string email)
        {
            var shops = await _shopRepository.GetShops(email);
            var collectionList = await _collectionManager.GetCollectionList();
            if (shops.Count == 0)
            {
                await UpdateShops(email);
                shops = await _shopRepository.GetShops(email);
            }
            return shops.Select(s => new ShopInfo
            {
                CollectionId = s.CollectionId,
                CollectionName = collectionList.FirstOrDefault(c => c.Id == s.CollectionId)?.Name,
                Amount = s.Amount,
                Price = s.Price
            }).ToList();
        }
        public async Task PurchasedShop(string email, int position)
        {
            var money = await _moneyManager.GetMoney(email);
            var shops = await _shopRepository.GetShops(email);
            if (position < 0 || position >= 6)
            {
                throw new Exception("Invalid position");
            }
            if (money < shops[position].Price)
            {
                throw new Exception("Not enough money");
            }
            await _shopRepository.PurchasedShop(email, position);
            await _moneyManager.RemoveMoney(email, shops[position].Price);
        }
        public async Task UpdateShops(string email)
        {
            var shopInfos = await GenerateShopInfos();
            var shops = await _shopRepository.GetShops(email);
            if (shops.Count != 6)
            {
                await _shopRepository.AddShops(email, await GenerateShopInfos());
            }
            else
            {
                await _shopRepository.UpdateShops(email, shopInfos);
            }
        }
        public async Task<List<ShopInfo>> GenerateShopInfos()
        {
            var commonCollections = await _collectionManager.GetCollectionListByRarity("common");
            var uncommonCollections = await _collectionManager.GetCollectionListByRarity("uncommon");
            var rareCollections = await _collectionManager.GetCollectionListByRarity("rare");
            var epicCollections = await _collectionManager.GetCollectionListByRarity("epic");
            var legendaryCollections = await _collectionManager.GetCollectionListByRarity("legendary");
            
            var commonPrice = await _collectionManager.GetRarityPrice("common");
            var uncommonPrice = await _collectionManager.GetRarityPrice("uncommon");
            var rarePrice = await _collectionManager.GetRarityPrice("rare");
            var epicPrice = await _collectionManager.GetRarityPrice("epic");
            var legendaryPrice = await _collectionManager.GetRarityPrice("legendary");

            var random = new Random();
            DBCollection? collection;

            var shopInfos = new List<ShopInfo>();


            //2 common
            for (int i = 0; i < 2; i++)
            {
                collection = commonCollections[random.Next(commonCollections.Count)];
                shopInfos.Add(new ShopInfo { CollectionId = collection.Id, CollectionName = collection.Name, Amount = 1, Price = commonPrice });
            }

            //1 uncommon
            collection = uncommonCollections[random.Next(uncommonCollections.Count)];
            shopInfos.Add(new ShopInfo { CollectionId = collection.Id, CollectionName = collection.Name, Amount = 1, Price = uncommonPrice });

            //1 rare
            collection = rareCollections[random.Next(rareCollections.Count)];
            shopInfos.Add(new ShopInfo { CollectionId = collection.Id, CollectionName = collection.Name, Amount = 1, Price = rarePrice });

            //1 epic
            collection = epicCollections[random.Next(epicCollections.Count)];
            shopInfos.Add(new ShopInfo { CollectionId = collection.Id, CollectionName = collection.Name, Amount = 1, Price = epicPrice });

            //1 legendary
            collection = legendaryCollections[random.Next(legendaryCollections.Count)];
            shopInfos.Add(new ShopInfo { CollectionId = collection.Id, CollectionName = collection.Name, Amount = 1, Price = legendaryPrice });

            return shopInfos;
        }
    }
}
