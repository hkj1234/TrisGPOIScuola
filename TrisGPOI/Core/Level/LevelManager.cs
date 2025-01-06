using TrisGPOI.Core.Level.Entities;
using TrisGPOI.Core.Level.Interfaces;

namespace TrisGPOI.Core.Level
{
    public class LevelManager : ILevelManager
    {
        private readonly IUserLevelRepository _userLevelRepository;
        public LevelManager(IUserLevelRepository userLevelRepository)
        {
            _userLevelRepository = userLevelRepository;
        }
        public async Task GainExperience(string email, int experience)
        {
            LevelAndExperience levelAndExperience = await _userLevelRepository.GetLevelAndExperience(email);
            levelAndExperience = await CalculateLevel(levelAndExperience, experience);
            await _userLevelRepository.SetLevelAndExperience(email, levelAndExperience);
        }
        private async Task<LevelAndExperience> CalculateLevel(LevelAndExperience levelAndExperience, int experience)
        {
            int level = levelAndExperience.Level;
            experience = levelAndExperience.Experience + experience;
            int nextLevelExperience = NextLevelExperience(level);
            while (experience >= nextLevelExperience)
            {
                level++;
                experience = experience - nextLevelExperience;
                nextLevelExperience = NextLevelExperience(level);
            }
            return new LevelAndExperience { Level = level, Experience = experience };
        }
        public int NextLevelExperience(int level)
        {
            return Math.Min((level) * 10, 200);
        }
    }
}

