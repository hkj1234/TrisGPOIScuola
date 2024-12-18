using System.Text.RegularExpressions;
using TrisGPOI.Core.JWT.Interfaces;
using TrisGPOI.Core.Mail.Interfaces;
using TrisGPOI.Core.OTP.Interfaces;
using TrisGPOI.Core.User.Entities;
using TrisGPOI.Core.User.Exceptions;
using TrisGPOI.Core.User.Interfaces;

namespace TrisGPOI.Core.User
{
    public class UserManager : IUserManager
    {
        private readonly IJWTManager _jWTManager;
        private readonly IUserRepository _userRepository;
        private readonly IMailManager _mailManager;
        private readonly IOTPManager _oTPManager;
        public UserManager(IJWTManager jWTManager, IUserRepository customersRepository, IMailManager mailManager, IOTPManager oTPManager)
        {
            _jWTManager = jWTManager;
            _userRepository = customersRepository;
            _mailManager = mailManager;
            _oTPManager = oTPManager;
        }
        public async Task RegisterAsync(UserRegister model)
        {
            if (await _userRepository.ExistActiveUser(model.Email) || await _userRepository.ExistActiveUser(model.Username))
            {
                throw new ExisitingEmailException();
            }
            if (!(CheckPassword(model.Password) && CheckUsername(model.Username) && CheckEmail(model.Email)))
            {
                throw new MalformedDataException();
            }
            await _mailManager.SendRegisterOtpEmailAsync(model.Email);

            if (! await _userRepository.ExistUser(model.Email))
            {
                await _userRepository.AddNewUserAsync(model);
            }
        }
        public async Task<string> LoginAsync(UserLogin model)
        {
            var customer = await _userRepository.FirstOrDefaultActiveUser(model.EmailOrUsername);
            if (customer == null || customer.Password != model.Password)
            {
                throw new WrongEmailOrPasswordException();
            }
            if (!customer.IsActive)
            {
                await _mailManager.SendRegisterOtpEmailAsync(customer.Email);
                throw new AccountNotActivedException();
            }

            return _jWTManager.JWTGenerate(customer.Email);
        }
        public async Task<string> VerifyOTP(string otp, string email)
        {
            await _oTPManager.CheckOTP(email, otp);

            await _userRepository.SetActiveUser(email);

            return _jWTManager.JWTGenerate(email);
        }
        public async Task LoginOTP(string email)
        {
            if (! await _userRepository.ExistActiveUser(email))
            {
                throw new NotExisitingEmailException();
            }
            await _mailManager.SendLoginOtpEmailAsync(email);
        }
        public bool CheckPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&.,])[A-Za-z\d@$!%*?&.,]{8,}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(password);
        }
        public bool CheckUsername(string username)
        {
            string pattern = @"^[a-zA-Z0-9_-]{3,}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(username);
        }
        public bool CheckEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
        public async Task PasswordDimenticata(string email)
        {
            if (!await _userRepository.ExistActiveUser(email))
            {
                throw new NotExisitingEmailException();
            }
            string PasswordGenerata = GenerateRandomPassword(16);
            await _mailManager.SendPasswordDimenticataEmailAsync(email, PasswordGenerata);
            await _userRepository.ChangeUserPassword(email, PasswordGenerata);
        }
        public string GenerateRandomPassword(int length)
        {
            const string upperChars = "ABCDEFGHJKLMNPQRSTUVWXYZ"; // Esclude 'O' e 'I'
            const string lowerChars = "abcdefghijkmnopqrstuvwxyz"; // Esclude 'l'
            const string digits = "0123456789";
            const string caratteriSpeciali = "@$!%*?&.,-_\"£&/()=+";

            string allChars = upperChars + lowerChars + digits + caratteriSpeciali;
            Random random = new Random();

            return new string(Enumerable.Repeat(allChars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
