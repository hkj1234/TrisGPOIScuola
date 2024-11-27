using TrisGPOI.Core.JWT.Interfaces;
using TrisGPOI.Core.User.Entities;
using TrisGPOI.Core.User.Exceptions;
using TrisGPOI.Core.User.Interfaces;

namespace TrisGPOI.Core.User
{
    public class UserManager : IUserManager
    {
        private readonly IJWTManager _jWTManager;
        private readonly IUserRepository _userRepository;
        public UserManager(IJWTManager jWTManager, IUserRepository customersRepository)
        {
            _jWTManager = jWTManager;
            _userRepository = customersRepository;
        }
        public async Task RegisterAsync(UserRegister model)
        {
            //da fare, va agigunto la roba se controllare username se contien esolo caratteri e numeri
            if (await _userRepository.ExistUser(model.Email) || await _userRepository.ExistUser(model.Username))
            {
                throw new ExisitingEmailException();
            }
            await _userRepository.AddNewUser(model);
        }
        public async Task<string?> LoginAsync(UserLogin model)
        {
            var customer = await _userRepository.FirstOrDefaultUser(model.EmailOrUsername);
            if (customer == null || customer.Password != model.Password)
            {
                throw new WrongEmailOrPasswordException();
            }

            return _jWTManager.JWTGenerate(customer.Email);
        }
    }
}
