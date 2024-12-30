using TrisGPOI.Core.Collection;
using TrisGPOI.Core.Collection.Interfaces;

namespace TrisGPOI.Controllers.Collection
{
    internal static class CollectionSetup
    {
        public static IServiceCollection AddCustomer(this IServiceCollection services)
        {
            services.AddScoped<ICollectionManager, CollectionManager>();
            return services;
        }
    }
}

