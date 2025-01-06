﻿using TrisGPOI.Core.Friend.Entities;
using TrisGPOI.Core.Friend.Exceptions;
using TrisGPOI.Core.Friend.Interfaces;
using TrisGPOI.Core.User.Exceptions;
using TrisGPOI.Core.User.Interfaces;

namespace TrisGPOI.Core.Friend
{
    public class FriendManager : IFriendManager
    {
        private readonly IFriendRepository _friendRepository;
        private readonly IUserRepository _userRepository;
        private readonly IReceiveBoxManager _receiveBoxManager;
        public FriendManager(IFriendRepository friendRepository, IUserRepository userRepository, IReceiveBoxManager receiveBoxManager)
        {
            _friendRepository = friendRepository;
            _userRepository = userRepository;
            _receiveBoxManager = receiveBoxManager;
        }
        public async Task<List<FriendInList>> GetFriends(string email)
        {
            return await _friendRepository.GetFriends(email);
        }
        public async Task<List<FriendRequestInList>> GetFriendsRequest(string email)
        {
            return await _friendRepository.GetFriendsRequest(email);
        }
        public async Task<List<FriendRequestInList>> GetFriendsRequestSent(string email)
        {
            return await _friendRepository.GetFriendsRequestSent(email);
        }
        public async Task SendFriendRequest(string email, string friendEmail)
        {
            if (!await _userRepository.ExistUser(friendEmail))
            {
                throw new NotExisitingEmailException();
            }
            if (await _friendRepository.ExistsFriendRequest(email, friendEmail))
            {
                throw new ExistFriendRequestException();
            }
            if (await _friendRepository.ExistsFriend(email, friendEmail))
            {
                throw new ExistFriendException();
            }
            await _friendRepository.SendFriendRequest(email, friendEmail);
        }
        public async Task AcceptFriendRequest(string email, string friendEmail)
        {
            if (await _friendRepository.ExistsFriendRequest(email, friendEmail))
            {
                await _friendRepository.AcceptFriendRequest(email, friendEmail);
                await _receiveBoxManager.SendReceiveBox("System", email, "Friend Request Accepted", "Your friend request to " + friendEmail + " has been accepted");
            }
            else
            {
                throw new NotExistingFriendRequestException();
            }
        }
        public async Task RejectFriendRequest(string email, string friendEmail)
        {
            if (await _friendRepository.ExistsFriendRequest(email, friendEmail))
            {
                await _friendRepository.RejectFriendRequest(email, friendEmail);
            }
            else
            {
                throw new NotExistingFriendRequestException();
            }
        }
        public async Task RemoveFriend(string email, string friendEmail)
        {
            if (await _friendRepository.ExistsFriend(email, friendEmail))
            {
                await _friendRepository.RemoveFriend(email, friendEmail);
                await _receiveBoxManager.SendReceiveBox("System", friendEmail, "Friend Removed", "Your friend " + email + " has been removed from your friends list");
            }
            else
            {
                throw new NotExistingFriendException();
            }
        }
    }
}
