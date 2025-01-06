namespace TrisGPOI.Core.Level.Interfaces
{
    public interface ILevelManager
    {
        Task GainExperience(string email, int experience);
        int NextLevelExperience(int level);
    }
}
