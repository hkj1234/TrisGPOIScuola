using TrisGPOI.Core.CPU;
using TrisGPOI.Core.CPU.Interfaces;
using TrisGPOI.Core.Game;
using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Database.Game;

namespace TrisGPOI.Controllers.Tris
{
    public static class TrisSetup
    {
        public static IServiceCollection AddTrisNormale(this IServiceCollection services)
        {
            services.AddScoped<IGameManager, GameplayManager>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<ICPUManagerFabric, CPUManagerFabric>();
            services.AddScoped<ITrisManagerFabric, TrisManagerFabric>();
            services.AddScoped<IGameVictoryManager, GameVictoryManager>();
            return services;
        }
    }
}
