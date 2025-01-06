namespace TrisGPOI.Core.Money.Interfaces
{
    public interface IUserMoneyXORepository
    {
        Task<int> GetMoney(string email);
        Task SetMoney(string email, int money);
        Task AddMoney(string email, int money);
        Task RemoveMoney(string email, int money);
    }
}
