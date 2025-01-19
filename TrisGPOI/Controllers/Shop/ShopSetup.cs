using TrisGPOI.Core.Shop;
using TrisGPOI.Core.Shop.Interfaces;
using TrisGPOI.Database.Shop;

namespace TrisGPOI.Controllers.Shop
{
    public static class ShopSetup
    {
        public static IServiceCollection AddShop(this IServiceCollection services)
        {
            services.AddScoped<IShopManager, ShopManager>();
            services.AddScoped<IShopRepository, ShopRepository>();
            return services;
        }
    }
}

