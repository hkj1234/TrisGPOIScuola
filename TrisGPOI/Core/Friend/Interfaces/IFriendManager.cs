using TrisGPOI.Core.Friend.Entities;

namespace TrisGPOI.Core.Friend.Interfaces
{
    public interface IFriendManager
    {
        Task<List<FriendInList>> GetFriends(string email);
        Task<List<FriendRequestInList>> GetFriendsRequest(string email);
        Task<List<FriendRequestInList>> GetFriendsRequestSent(string email);
        Task SendFriendRequest(string email, string friendEmail);
        Task AcceptFriendRequest(string email, string friendEmail);
        Task RejectFriendRequest(string email, string friendEmail); 
        Task RemoveFriend(string email, string friendEmail);
    }
}

