using TrisGPOI.Core.Money.Interfaces;

namespace TrisGPOI.Core.Money
{
    public class MoneyXOManager : IMoneyXOManager
    {
        private readonly IUserMoneyXORepository _userMoneyXORepository;
        public MoneyXOManager(IUserMoneyXORepository userMoneyXORepository)
        {
            _userMoneyXORepository = userMoneyXORepository;
        }
        public async Task<int> GetMoney(string email)
        {
            return await _userMoneyXORepository.GetMoney(email);
        }
        public async Task AddMoney(string email, int money)
        {
            await _userMoneyXORepository.AddMoney(email, money);
        }
        public async Task RemoveMoney(string email, int money)
        {
            await _userMoneyXORepository.RemoveMoney(email, money);
        }
    }
}
