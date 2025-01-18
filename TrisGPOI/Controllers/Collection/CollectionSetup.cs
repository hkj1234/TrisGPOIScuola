using TrisGPOI.Core.Collection;
using TrisGPOI.Core.Collection.Interfaces;
using TrisGPOI.Database.Collection;

namespace TrisGPOI.Controllers.Collection
{
    internal static class CollectionSetup
    {
        public static IServiceCollection AddCollection(this IServiceCollection services)
        {
            services.AddScoped<ICollectionManager, CollectionManager>();
            services.AddScoped<ICollectionRepository, CollectionRepository>();
            services.AddScoped<ICollectionInventoryRepository, CollectionInventoryRepository>();
            services.AddScoped<ICollectionInventoryManager, CollectionInventoryManager>();
            return services;
        }
    }
}

