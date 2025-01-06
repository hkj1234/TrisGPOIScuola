using TrisGPOI.Core.ReceiveBox.Entities;

namespace TrisGPOI.Core.ReceiveBox.Interfaces
{
    public interface IReceiveBoxRepository
    {
        Task<List<DBReceiveBox>> GetReceiveBox(string email);
        Task SendReceiveBox(string sender, string receiver, string title, string message);
        Task DeleteReceiveBox(int Id);
        Task ReadReceiveBox(int Id);
        Task<bool> ExistReceiveBox(int Id);
        Task<bool> ExistUnreadMailBox(string email);
    }
}
