using Microsoft.EntityFrameworkCore;
using TrisGPOI.Core.Friend.Entities;
using TrisGPOI.Core.Friend.Interfaces;
using TrisGPOI.Database.Context;
using TrisGPOI.Database.Friend.Entities;

namespace TrisGPOI.Database.Friend
{
    public class FriendRepository : IFriendRepository
    {
        private readonly IDbContextFactory _dbContextFactory;
        public FriendRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<FriendInList>> GetFriends(string email)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            return await _context.Friend.Where(f => f.User1Email == email || f.User2Email == email)
                                    .Select(f => new FriendInList {
                                        Email = f.User1Email == email ? f.User2Email : f.User1Email,
                                        Username = f.User1Email == email ? f.User2.Username : f.User1.Username, 
                                        FotoProfilo = f.User1Email == email ? f.User2.FotoProfilo : f.User1.FotoProfilo,
                                        State = f.User1Email == email ? f.User2.Status : f.User1.Status,
                                    })
                                    .ToListAsync();
        }
        public async Task<List<FriendRequestInList>> GetFriendsRequest(string email)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            return await _context.FriendRequest.Where(f => f.ReceiverEmail == email)
                                    .Select(f => new FriendRequestInList {
                                        SenderEmail = f.SenderEmail,
                                        ReceiverEmail = f.ReceiverEmail,
                                    })
                                    .ToListAsync();
        }
            public async Task<List<FriendRequestInList>> GetFriendsRequestSent(string email)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            return await _context.FriendRequest.Where(f => f.SenderEmail == email)
                                    .Select(f => new FriendRequestInList {
                                        SenderEmail = f.SenderEmail,
                                        ReceiverEmail = f.ReceiverEmail,
                                    })
                                    .ToListAsync();
        }
        public async Task SendFriendRequest(string email, string friendEmail)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            _context.FriendRequest.Add(new DBFriendRequest { SenderEmail = email, ReceiverEmail = friendEmail });
            await _context.SaveChangesAsync();
        }
        public async Task AcceptFriendRequest(string email, string friendEmail)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            var friendRequest = _context.FriendRequest.FirstOrDefault(f => f.SenderEmail == friendEmail && f.ReceiverEmail == email);
            if (friendRequest != null)
            {
                _context.FriendRequest.Remove(friendRequest);
                _context.Friend.Add(new DBFriend { User1Email = email, User2Email = friendEmail });
                await _context.SaveChangesAsync();
            }
        }
        public async Task RejectFriendRequest(string email, string friendEmail)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            var friendRequest = _context.FriendRequest.FirstOrDefault(f => f.SenderEmail == friendEmail && f.ReceiverEmail == email);
            if (friendRequest != null)
            {
                _context.FriendRequest.Remove(friendRequest);
                await _context.SaveChangesAsync();
            }
        }
        public async Task RemoveFriend(string email, string friendEmail)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            var friend = _context.Friend.FirstOrDefault(f => f.User1Email == email && f.User2Email == friendEmail || f.User1Email == friendEmail && f.User2Email == email);
            if (friend != null)
            {
                _context.Friend.Remove(friend);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> ExistsFriendRequest(string email, string friendEmail)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            return _context.FriendRequest.Any(f => f.SenderEmail == email && f.ReceiverEmail == friendEmail);
        }
        public async Task<bool> ExistsFriend(string email, string friendEmail)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            return _context.Friend.Any(f => f.User1Email == email && f.User2Email == friendEmail || f.User1Email == friendEmail && f.User2Email == email);
        }
    }
}
