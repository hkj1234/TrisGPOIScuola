using TrisGPOI.Core.User.Entities;
using TrisGPOI.Database.User.Entities;

namespace TrisGPOI.Core.User.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ExistUser(string emailOrUsername);
        Task<DBUser> FirstOrDefaultUser(string emailOrUsername);
        Task AddNewUserAsync(UserRegister model);
        Task SetActiveUser(string email);
    }
}
