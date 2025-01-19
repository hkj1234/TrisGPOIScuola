using TrisGPOI.Core.Shop.Interfaces;

namespace TrisGPOI.Core.Shop
{
    public class ShopManager : IShopManager
    {
        private readonly IShopRepository _shopRepository;
        private readonly ICollectionManager _collectionManager;
        public ShopManager(IShopRepository shopRepository, ICollectionManager collectionManager)
        {
            _shopRepository = shopRepository;
            _collectionManager = collectionManager;
        }
        public async Task<List<ShopInfo>> GetShops(string email)
        {
            var shops = await _shopRepository.GetShops(email);
            return shops.Select(s => new ShopInfo
            {
                CollectionId = s.CollectionId,
                Amount = s.Amount,
                Price = s.Price
            }).ToList();
        }
        public async Task PurchasedShop(string email, int position)
        {
            if (position < 0 || position >= 6)
            {
                throw new Exception("Invalid position");
            }
            await _shopRepository.PurchasedShop(email, position);
        }
        public async Task UpdateShops(string email, List<ShopInfo> shopInfos)
        {
            await _shopRepository.UpdateShops(email, shopInfos);
        }
        public List<ShopInfo> GenerateShopInfos()
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

            return new List<ShopInfo>
            {
                new ShopInfo { CollectionId = "1", Amount = 100, Price = 100 },
                new ShopInfo { CollectionId = "2", Amount = 100, Price = 100 },
                new ShopInfo { CollectionId = "3", Amount = 100, Price = 100 },
                new ShopInfo { CollectionId = "4", Amount = 100, Price = 100 },
                new ShopInfo { CollectionId = "5", Amount = 100, Price = 100 },
                new ShopInfo { CollectionId = "6", Amount = 100, Price = 100 },
            };
        }
    }
}
