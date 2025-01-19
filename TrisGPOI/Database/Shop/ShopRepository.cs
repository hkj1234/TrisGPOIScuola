using Microsoft.EntityFrameworkCore;
using TrisGPOI.Core.Shop.Entities;
using TrisGPOI.Core.Shop.Interfaces;
using TrisGPOI.Database.Context;
using TrisGPOI.Database.Shop.Entities;

namespace TrisGPOI.Database.Shop
{
    public class ShopRepository : IShopRepository
    {
        private readonly IDbContextFactory _context;

        public ShopRepository(IDbContextFactory context)
        {
            _context = context;
        }

        public async Task<List<DBShop>> GetShops(string email)
        {
            using var context = _context.CreateMySQLDbContext();
            var shops = await context.Shops.Where(s => s.Email == email).OrderBy(s => s.Id).ToListAsync();
            return shops;
        }

        public async Task PurchasedShop(string email, int position)
        {
            using var context = _context.CreateMySQLDbContext();
            var shops = await context.Shops.Where(s => s.Email == email).OrderBy(s => s.Id).ToListAsync();
            if (shops.Count <= position)
            {
                return;
            }
            shops[position].Purchased = true;
            await context.SaveChangesAsync();
        }

        public async Task UpdateShops(string email, List<ShopInfo> shopInfos)
        {
            using var context = _context.CreateMySQLDbContext();
            var shops = await context.Shops.Where(s => s.Email == email).OrderBy(s => s.Id).ToListAsync();
            for (int i = 0; i < shops.Count; i++)
            {
                shops[i].CollectionId = shopInfos[i].CollectionId;
                shops[i].Amount = shopInfos[i].Amount;
                shops[i].Price = shopInfos[i].Price;
                shops[i].Purchased = false;
            }
            await context.SaveChangesAsync();
        }
    }
}
