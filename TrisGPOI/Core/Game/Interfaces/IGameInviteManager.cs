using TrisGPOI.Database.Game.Entities;

namespace TrisGPOI.Core.Game.Interfaces
{
    public interface IGameInviteManager
    {
        Task<DBGameInvite> GetInvitesByEmail(string email);
        Task<bool> AnyInvite(string email);
        Task<bool> AnyInviteByInvitedEmail(string invitedEmail);
        Task<bool> AnyInviteByInviterEmail(string inviterEmail);
        Task InviteGame(string inviterEmail, string invitedEmail, string gameType);
        Task AcceptInvite(string invitedEmail);
        Task DeclineInvite(string invitedEmail);
        Task DeleteInvite(string inviterEmail);
    }
}
