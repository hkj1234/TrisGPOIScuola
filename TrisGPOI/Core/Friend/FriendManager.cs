﻿using TrisGPOI.Core.Friend.Entities;
using TrisGPOI.Core.Friend.Exceptions;
using TrisGPOI.Core.Friend.Interfaces;

namespace TrisGPOI.Core.Friend
{
    public class FriendManager : IFriendManager
    {
        private readonly IFriendRepository _friendRepository;
        public FriendManager(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
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
            }
            else
            {
                throw new NotExistingFriendException();
            }
        }
    }
}
