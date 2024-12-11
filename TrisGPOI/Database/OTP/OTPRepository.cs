using Microsoft.EntityFrameworkCore;
using TrisGPOI.Core.Mail.Interfaces;
using TrisGPOI.Core.OTP.Interfaces;
using TrisGPOI.Database.Context;
using TrisGPOI.Database.OTP.Entities;

namespace TrisGPOI.Database.OTP
{
    public class OTPRepository : IOTPRepository
    {
        private readonly IDbContextFactory _dbContextFactory;
        public OTPRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<bool> ExistOTPEmail(string email)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            return await _context.OTP.AnyAsync(x => x.Email == email); 
        }

        public async Task AddNewOTP(string email, string otp)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            if (await ExistOTPEmail(email))
            {   //se esiste aggiorni database
                DBOtpEntity OTP = await _context.OTP.FirstOrDefaultAsync(x => x.Email == email);
                OTP.OtpCode = otp;
                _context.OTP.Update(OTP);
                await _context.SaveChangesAsync();
            }
            else
            {   //se non esiste aggiungi al database
                DBOtpEntity newOTP = new DBOtpEntity
                {
                    Email = email,
                    OtpCode = otp,
                    ExpiryTime = DateTime.UtcNow.AddMinutes(10)
                };
                _context.OTP.Add(newOTP);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> CheckOTP(string email, string otp)
        {
            await using var _context = _dbContextFactory.CreateMySQLDbContext();
            return _context.OTP.Any(x => x.Email == email && x.ExpiryTime >= DateTime.UtcNow && x.OtpCode == otp);
        }
    }
}
