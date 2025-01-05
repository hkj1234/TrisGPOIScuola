using TrisGPOI.Core.Friend;
using TrisGPOI.Core.Friend.Interfaces;
using TrisGPOI.Database.Friend;

namespace TrisGPOI.Controllers.Friend
{
    public static class FriendSetup
    {
        public static IServiceCollection AddFriend(this IServiceCollection services)
        {
            services.AddScoped<IFriendManager, FriendManager>();
            services.AddScoped<IFriendRepository, FriendRepository>();
            return services;
        }
    }
}
