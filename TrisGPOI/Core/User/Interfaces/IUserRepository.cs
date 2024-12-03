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
    }
}
