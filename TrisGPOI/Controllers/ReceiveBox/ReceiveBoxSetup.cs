using TrisGPOI.Core.ReceiveBox;
using TrisGPOI.Core.ReceiveBox.Interfaces;
using TrisGPOI.Database.ReceiveBox;

namespace TrisGPOI.Controllers.ReceiveBox
{
    public static class ReceiveBoxSetup
    {
        public static IServiceCollection AddReceiveBox(this IServiceCollection services)
        {
            services.AddScoped<IReceiveBoxManager, ReceiveBoxManager>();
            services.AddScoped<IReceiveBoxRepository, ReceiveBoxRepository>();
            return services;
        }
    }
}

