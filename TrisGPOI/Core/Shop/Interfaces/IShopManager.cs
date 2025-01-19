using TrisGPOI.Core.Shop.Entities;

namespace TrisGPOI.Core.Shop.Interfaces
{
    public interface IShopManager
    {
        Task<List<ShopInfo>> GetShops(string email);
        Task PurchasedShop(string email, int position);
        Task UpdateShops(string email);
    }
}
