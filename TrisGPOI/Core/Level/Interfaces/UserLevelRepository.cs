using TrisGPOI.Core.Level.Entities;

namespace TrisGPOI.Core.Level.Interfaces
{
    public interface IUserLevelRepository
    {
        Task<LevelAndExperience> GetLevelAndExperience(string email);
        Task SetLevelAndExperience(string email, LevelAndExperience levelAndExperience);
    }
}
