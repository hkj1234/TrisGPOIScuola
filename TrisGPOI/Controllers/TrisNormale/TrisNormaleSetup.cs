using TrisGPOI.Core.Game;
using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Database.Game;

namespace TrisGPOI.Controllers.TrisNormale
{
    public static class TrisNormaleSetup
    {
        public static IServiceCollection AddTrisNormale(this IServiceCollection services)
        {
            services.AddScoped<IGameManager, GameManager>();
            services.AddScoped<IGameRepository, GameRepository>();
            return services;
        }
    }
}
