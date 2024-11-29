using TrisGPOI.Core.JWT;
using TrisGPOI.Core.JWT.Interfaces;
using TrisGPOI.Core.Mail;
using TrisGPOI.Core.Mail.Interfaces;
using TrisGPOI.Core.OTP;
using TrisGPOI.Core.OTP.Interfaces;
using TrisGPOI.Core.User;
using TrisGPOI.Core.User.Interfaces;
using TrisGPOI.Database.OTP;
using TrisGPOI.Database.User;

namespace TrisGPOI.Controllers.User
{
    internal static class UserSetup
    {
        public static IServiceCollection AddCustomer(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IJWTManager, JWTManager>();
            services.AddScoped<IGetOptionManager, GetOptionManager>();
            services.AddScoped<IOTPRepository, OTPRepository>();
            services.AddScoped<IOTPManager, OTPManager>();
            services.AddScoped<IMailManager, MailManager>();
            return services;
        }
    }
}

