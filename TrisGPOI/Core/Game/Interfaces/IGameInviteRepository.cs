using TrisGPOI.Database.Game.Entities;

namespace TrisGPOI.Core.Game.Interfaces
{
    public interface IGameInviteRepository
    {
        Task<DBGameInvite> GetInvitesByEmail(string email);
        Task<bool> AnyInvite(string email);
        Task<bool> AnyInviteByInvitedEmail(string invitedEmail);
        Task InviteGame(string inviterEmail, string invitedEmail, string gameType);
        Task DeleteInvite(string email);
        Task<DBGameInvite> GetInvitesByInvitedEmail(string email);
    }
}
