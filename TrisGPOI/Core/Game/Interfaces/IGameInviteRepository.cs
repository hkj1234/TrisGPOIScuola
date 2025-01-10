using TrisGPOI.Database.Game.Entities;

namespace TrisGPOI.Core.Game.Interfaces
{
    public interface IGameInviteRepository
    {
        Task<DBGameInvite> GetInvitesByEmail(string email);
        Task<bool> AnyInvite(string email);
        Task<bool> AnyInviteByInvitedEmail(string invitedEmail);
        Task<bool> AnyInviteByInviterEmail(string inviterEmail);
        Task InviteGame(string inviterEmail, string invitedEmail, string gameType);
        Task DeleteInvite(int id);
        Task<int> GetIdByInvitedEmail(string invitedEmail);
        Task<int> GetIdByInviterEmail(string inviterEmail);
    }
}
