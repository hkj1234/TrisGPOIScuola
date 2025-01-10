using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Database.Game.Entities;

namespace TrisGPOI.Core.Game
{
    public class GameInviteManager : IGameInviteManager
    {
        private readonly IGameInviteRepository _gameInviteRepository;
        private readonly IGameRepository _gameRepository;
        private readonly ITrisManagerFabric _trisManagerFabric;
        public GameInviteManager(IGameInviteRepository gameInviteRepository, IGameRepository gameRepository, ITrisManagerFabric trisManagerFabric)
        {
            _gameInviteRepository = gameInviteRepository;
            _gameRepository = gameRepository;
            _trisManagerFabric = trisManagerFabric;
        }
        public async Task<DBGameInvite> GetInvitesByEmail(string email)
        {
            return await _gameInviteRepository.GetInvitesByEmail(email);
        }
        public async Task<bool> AnyInvite(string email)
        {
            return await _gameInviteRepository.AnyInvite(email);
        }
        public async Task InviteGame(string inviterEmail, string invitedEmail, string gameType)
        {
            if (await AnyInvite(invitedEmail))
            {
                throw new Exception("Invite already exists");
            }
            await _gameInviteRepository.InviteGame(inviterEmail, invitedEmail, gameType);
        }
        public async Task AcceptInvite(string invitedEmail)
        {
            await DeclineInvite(invitedEmail);
            var invite = await _gameInviteRepository.GetInvitesByEmail(invitedEmail);
            var trisManager = _trisManagerFabric.CreateTrisManager(invite.GameType);
            var emptyBoard = trisManager.CreateEmptyBoard();
            await _gameRepository.FriendGame(invite.InviterEmail, invite.InvitedEmail, invite.GameType, emptyBoard);
        }
        public async Task DeclineInvite(string invitedEmail)
        {
            if (!await AnyInvite(invitedEmail))
            {
                throw new Exception("Invite not found");
            }
            await _gameInviteRepository.DeleteInvite(invitedEmail);
        }
    }
}

