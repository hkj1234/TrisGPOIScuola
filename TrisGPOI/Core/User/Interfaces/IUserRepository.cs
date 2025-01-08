using TrisGPOI.Core.User.Entities;
using TrisGPOI.Database.User.Entities;

namespace TrisGPOI.Core.User.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ExistActiveUser(string emailOrUsername);
        Task<DBUser> FirstOrDefaultActiveUser(string emailOrUsername);
        Task AddNewUserAsync(UserRegister model);
        Task SetActiveUser(string email);
        Task ChangeUserPassword(string email, string password);
        Task<bool> ExistUser(string emailOrUsername);
        Task ChangeUserFoto(string email, string StringaFoto);
        Task ChangeUserDescription(string email, string description);
        Task ChangeUserStatus(string email, string Status);
        Task<string> GetEmailByUsername(string username);
        Task<int> GetUserStatusNumber(string email);
        Task AddUserStatusNumber(string email);
        Task SubUserStatusNumber(string email);
        Task ResetUserStatusNumber(string email);
        Task UpdateLastLogin(string email);
        Task<DateTime> GetLastLogin(string email);
    }
}
