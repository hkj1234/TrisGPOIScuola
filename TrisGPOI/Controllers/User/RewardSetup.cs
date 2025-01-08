using TrisGPOI.Core.Level;
using TrisGPOI.Core.Level.Interfaces;
using TrisGPOI.Core.Money;
using TrisGPOI.Core.Money.Interfaces;
using TrisGPOI.Core.Reward;
using TrisGPOI.Core.Reward.Interfaces;
using TrisGPOI.Database.User;

namespace TrisGPOI.Controllers.User
{
    public static class RewardSetup
    {
        public static IServiceCollection AddReward(this IServiceCollection services)
        {
            services.AddScoped<IUserRewardRepository, UserRewardRepository>();
            services.AddScoped<IRewardManager, RewardManager>();
            services.AddScoped<IMoneyXOManager, MoneyXOManager>();
            services.AddScoped<ILevelManager, LevelManager>();
            services.AddScoped<IUserLevelRepository, UserLevelRepository>();
            services.AddScoped<IUserMoneyXORepository, UserMoneyXORepository>();
            return services;
        }
    }
}
