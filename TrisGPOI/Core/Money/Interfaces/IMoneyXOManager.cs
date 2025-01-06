namespace TrisGPOI.Core.Money.Interfaces
{
    public interface IMoneyXOManager
    {
        Task<int> GetMoney(string email);
        Task AddMoney(string email, int money);
        Task RemoveMoney(string email, int money);
    }
}
