using TrisGPOI.Core.Shop.Entities;
using TrisGPOI.Database.Shop.Entities;

namespace TrisGPOI.Core.Shop.Interfaces
{
    public interface IShopRepository
    {
        Task<List<DBShop>> GetShops(string email);
        Task PurchasedShop(string email, int position);
        Task UpdateShops(string email, List<ShopInfo> shopInfos);
        Task AddShops(string email, List<ShopInfo> shopInfos);
    }
}
