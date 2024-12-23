using TrisGPOI.Database.User.Entities;

namespace TrisGPOI.Core.User.Interfaces
{
    public interface IUserVittorieRepository
    {
        Task<DBUserVittoriePVP> FindVittorieWithEmail(string email);
        Task<DBUserVittoriePVP> AddNewVittorie(string email);
        Task UserVictory(string email, string gameType);
        Task UserLose(string email, string gameType);
        Task UserDraw(string email, string gameType);

    }
}
