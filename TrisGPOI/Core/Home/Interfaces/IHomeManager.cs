namespace TrisGPOI.Core.Home.Interfaces
{
    public interface IHomeManager
    {
        Task SetOnlineTemperaly(string email);
        Task SetOffline(string email);
        Task SetPlaying(string email);
        Task<string> GetUserStatus(string email);
        Task ChangeUserStatus(string email, string status);
    }
}
