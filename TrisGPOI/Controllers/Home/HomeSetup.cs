using TrisGPOI.Core.Home.Interfaces;
using TrisGPOI.Core.Home;

namespace TrisGPOI.Controllers.Home
{
    public static class HomeSetup
    {
        public static IServiceCollection AddHome(this IServiceCollection services)
        {
            services.AddScoped<IHomeManager, HomeManager>();
            return services;
        }
    }
}
