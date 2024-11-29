using TrisGPOI.Core.OTP.Exceptions;
using TrisGPOI.Core.OTP.Interfaces;
using TrisGPOI.Database.OTP.Entities;

namespace TrisGPOI.Core.OTP
{
    public class OTPManager : IOTPManager
    {
        private readonly IOTPRepository _oTPRepository;
        public OTPManager(IOTPRepository oTPRepository)
        {
            _oTPRepository = oTPRepository;
        }
        public async Task AddNewOTP(string email, string otp)
        {
            await _oTPRepository.AddNewOTP(email, otp);
        }
        public string GenerateOtp()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
        public async Task CheckOTP(string email, string otp)
        {
            bool ris = await _oTPRepository.CheckOTP(email, otp);
            if (!ris)
            {
                throw new WrongEmailOrOTPExeption();
            }
        }
    }
}
