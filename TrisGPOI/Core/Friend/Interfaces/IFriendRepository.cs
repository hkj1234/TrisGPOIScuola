using TrisGPOI.Core.Friend.Entities;
using TrisGPOI.Database.Friend.Entities;

namespace TrisGPOI.Core.Friend.Interfaces
{
    public interface IFriendRepository
    {
        Task<List<FriendInList>> GetFriends(string email);
        Task<List<FriendRequestInList>> GetFriendsRequest(string email);
        Task<List<FriendRequestInList>> GetFriendsRequestSent(string email);
        Task SendFriendRequest(string email, string friendEmail);
        Task AcceptFriendRequest(string email, string friendEmail);
        Task DeleteFriendRequest(string email, string friendEmail); 
        Task RemoveFriend(string email, string friendEmail);
        Task<bool> ExistsFriendRequest(string email, string friendEmail);
        Task<bool> ExistsFriend(string email, string friendEmail);
    }
}

