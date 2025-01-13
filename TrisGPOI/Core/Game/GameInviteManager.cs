using TrisGPOI.Core.Game.Interfaces;
using TrisGPOI.Core.Home.Interfaces;
using TrisGPOI.Database.Game.Entities;

namespace TrisGPOI.Core.Game
{
    public class GameInviteManager : IGameInviteManager
    {
        private readonly IGameInviteRepository _gameInviteRepository;
        private readonly IGameRepository _gameRepository;
        private readonly ITrisManagerFabric _trisManagerFabric;
        private readonly IHomeManager _homeManager;
        public GameInviteManager(IGameInviteRepository gameInviteRepository, IGameRepository gameRepository, ITrisManagerFabric trisManagerFabric, IHomeManager homeManager)
        {
            _gameInviteRepository = gameInviteRepository;
            _gameRepository = gameRepository;
            _trisManagerFabric = trisManagerFabric;
            _homeManager = homeManager;
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
            var inviterStatus = await _homeManager.GetUserStatus(inviterEmail);
            var invitedStatus = await _homeManager.GetUserStatus(invitedEmail);

            if (await AnyInvite(invitedEmail) || await AnyInvite(inviterEmail))
            {
                throw new Exception("Invite already exists");
            }

            //da fare, da decomentare
            /*
            if (inviterStatus == "Offline" || invitedStatus == "Offline")
            {
                throw new Exception("User is offline");
            }

            if (inviterStatus == "Playing" || invitedStatus == "Playing")
            {
                throw new Exception("User is in game");
            }
            */

            await _gameInviteRepository.InviteGame(inviterEmail, invitedEmail, gameType);
        }
        public async Task AcceptInvite(string invitedEmail)
        {
            var invite = await _gameInviteRepository.GetInvitesByInvitedEmail(invitedEmail);
            if (invite == null)
            {
                throw new Exception("Invite not found");
            }
            await DeleteInvite(invitedEmail);
            var trisManager = _trisManagerFabric.CreateTrisManager(invite.GameType);
            var emptyBoard = trisManager.CreateEmptyBoard();
            await _gameRepository.FriendGame(invite.InviterEmail, invite.InvitedEmail, invite.GameType, emptyBoard);
        }
        public async Task DeclineInvite(string invitedEmail)
        {
            await DeleteInvite(invitedEmail);
        }
        public async Task DeleteInvite(string email)
        {
            if (!await AnyInvite(email))
            {
                throw new Exception("Invite not found");
            }
            await _gameInviteRepository.DeleteInvite(email);
        }
        public async Task<bool> IsInvited(string email)
        {
            bool ris = true;
            var invites = await GetInvitesByEmail(email);
            if (invites == null || invites.InvitedEmail != email)
            {
                ris = false;
            }
            return ris;
        }
        public async Task<bool> IsInviter(string email)
        {
            bool ris = true;
            var invites = await GetInvitesByEmail(email);
            if (invites == null || invites.InviterEmail != email)
            {
                ris = false;
            }
            return ris;
        }
    }
}

