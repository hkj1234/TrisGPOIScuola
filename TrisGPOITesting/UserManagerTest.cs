using Moq;
using TrisGPOI.Core.JWT.Interfaces;
using TrisGPOI.Core.Mail;
using TrisGPOI.Core.Mail.Interfaces;
using TrisGPOI.Core.OTP.Exceptions;
using TrisGPOI.Core.OTP.Interfaces;
using TrisGPOI.Core.User;
using TrisGPOI.Core.User.Entities;
using TrisGPOI.Core.User.Exceptions;
using TrisGPOI.Core.User.Interfaces;
using TrisGPOI.Database.User.Entities;

namespace TrisGPOIManagerTesting
{
    public class UserManagerTest
    {
        private readonly Mock<IJWTManager> _mockJWTManager;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IMailManager> _mockMailManager;
        private readonly Mock<IOTPManager> _mockOTPManager;
        public UserManagerTest()
        {
            _mockJWTManager = new Mock<IJWTManager>(MockBehavior.Strict);
            _mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            _mockMailManager = new Mock<IMailManager>(MockBehavior.Strict);
            _mockOTPManager = new Mock<IOTPManager>(MockBehavior.Strict);
        }

        [Test]
        public void RegisterWithExistingEmail()
        {
            var email = "test@gmail.com";
            var username = "test";
            var userRegister = new UserRegister
            {
                Email = email,
                Username = username,
                Password = "Testing123...",
            };
            var manager = new UserManager(_mockJWTManager.Object, _mockUserRepository.Object, _mockMailManager.Object, _mockOTPManager.Object);

            _mockUserRepository.Setup(x => x.ExistActiveUser(email)).ReturnsAsync(false);
            _mockUserRepository.Setup(x => x.ExistActiveUser(username)).ReturnsAsync(true);

            async Task Act()
            {
                await manager.RegisterAsync(userRegister);
            }

            Assert.ThrowsAsync<ExisitingEmailException>(Act);
        }

        [Test]
        public void RegisterWithExistingUsername()
        {
            var email = "test@gmail.com";
            var username = "test";
            var userRegister = new UserRegister
            {
                Email = email,
                Username = username,
                Password = "Testing123...",
            };
            var manager = new UserManager(_mockJWTManager.Object, _mockUserRepository.Object, _mockMailManager.Object, _mockOTPManager.Object);

            _mockUserRepository.Setup(x => x.ExistActiveUser(email)).ReturnsAsync(true);
            _mockUserRepository.Setup(x => x.ExistActiveUser(username)).ReturnsAsync(false);

            async Task Act()
            {
                await manager.RegisterAsync(userRegister);
            }

            Assert.ThrowsAsync<ExisitingEmailException>(Act);
        }

        [Test]
        public void RegisterWithMalformedEmail()
        {
            var email = "testgmail.com";
            var username = "test";
            var userRegister = new UserRegister
            {
                Email = email,
                Username = username,
                Password = "Testing123...",
            };
            var manager = new UserManager(_mockJWTManager.Object, _mockUserRepository.Object, _mockMailManager.Object, _mockOTPManager.Object);

            _mockUserRepository.Setup(x => x.ExistActiveUser(email)).ReturnsAsync(false);
            _mockUserRepository.Setup(x => x.ExistActiveUser(username)).ReturnsAsync(false);

            async Task Act()
            {
                await manager.RegisterAsync(userRegister);
            }

            Assert.ThrowsAsync<MalformedDataException>(Act);
        }

        [Test]
        public void RegisterWithMalformedUsername()
        {
            var email = "test@gmail.com";
            var username = "te";
            var userRegister = new UserRegister
            {
                Email = email,
                Username = username,
                Password = "Testing123...",
            };
            var manager = new UserManager(_mockJWTManager.Object, _mockUserRepository.Object, _mockMailManager.Object, _mockOTPManager.Object);

            _mockUserRepository.Setup(x => x.ExistActiveUser(email)).ReturnsAsync(false);
            _mockUserRepository.Setup(x => x.ExistActiveUser(username)).ReturnsAsync(false);

            async Task Act()
            {
                await manager.RegisterAsync(userRegister);
            }

            Assert.ThrowsAsync<MalformedDataException>(Act);
        }

        [Test]
        public void RegisterWithMalformedPassword()
        {
            var email = "test@gmail.com";
            var username = "test";
            var userRegister = new UserRegister
            {
                Email = email,
                Username = username,
                Password = "errorError",
            };
            var manager = new UserManager(_mockJWTManager.Object, _mockUserRepository.Object, _mockMailManager.Object, _mockOTPManager.Object);

            _mockUserRepository.Setup(x => x.ExistActiveUser(email)).ReturnsAsync(false);
            _mockUserRepository.Setup(x => x.ExistActiveUser(username)).ReturnsAsync(false);

            async Task Act()
            {
                await manager.RegisterAsync(userRegister);
            }

            Assert.ThrowsAsync<MalformedDataException>(Act);
        }

        [Test]
        public void RegisterSuccessWithNotExistingEmail()
        {
            var email = "test@gmail.com";
            var username = "test";
            var userRegister = new UserRegister
            {
                Email = email,
                Username = username,
                Password = "Testing123...",
            };
            var manager = new UserManager(_mockJWTManager.Object, _mockUserRepository.Object, _mockMailManager.Object, _mockOTPManager.Object);

            _mockUserRepository.Setup(x => x.ExistActiveUser(email)).ReturnsAsync(false);
            _mockUserRepository.Setup(x => x.ExistActiveUser(username)).ReturnsAsync(false);
            _mockMailManager.Setup(x => x.SendRegisterOtpEmailAsync(email)).Returns(Task.CompletedTask);
            _mockUserRepository.Setup(x => x.ExistUser(email)).ReturnsAsync(false);
            _mockUserRepository.Setup(x => x.AddNewUserAsync(userRegister)).Returns(Task.CompletedTask);


            async Task Act()
            {
                await manager.RegisterAsync(userRegister);
            }

            Assert.DoesNotThrowAsync(Act);
        }

        [Test]
        public void RegisterSuccessWithExistingEmail()
        {
            var email = "test@gmail.com";
            var username = "test";
            var userRegister = new UserRegister
            {
                Email = email,
                Username = username,
                Password = "Testing123...",
            };
            var manager = new UserManager(_mockJWTManager.Object, _mockUserRepository.Object, _mockMailManager.Object, _mockOTPManager.Object);

            _mockUserRepository.Setup(x => x.ExistActiveUser(email)).ReturnsAsync(false);
            _mockUserRepository.Setup(x => x.ExistActiveUser(username)).ReturnsAsync(false);
            _mockMailManager.Setup(x => x.SendRegisterOtpEmailAsync(email)).Returns(Task.CompletedTask);
            _mockUserRepository.Setup(x => x.ExistUser(email)).ReturnsAsync(true);

            async Task Act()
            {
                await manager.RegisterAsync(userRegister);
            }

            Assert.DoesNotThrowAsync(Act);
        }

        [Test]
        public void LoginWithNotExistingEmail()
        {
            var email = "test@gmail.com";
            var userLogin = new UserLogin
            {
                EmailOrUsername = email,
                Password = "Testing123...",
            };
            var UserResult = new DBUser();

            var manager = new UserManager(_mockJWTManager.Object, _mockUserRepository.Object, _mockMailManager.Object, _mockOTPManager.Object);

            _mockUserRepository.Setup(x => x.FirstOrDefaultActiveUser(It.IsAny<string>())).ReturnsAsync(UserResult);

            async Task Act()
            {
                await manager.LoginAsync(userLogin);
            }

            Assert.ThrowsAsync<WrongEmailOrPasswordException>(Act);
        }

        [Test]
        public void LoginWithWrongPassword()
        {
            var email = "test@gmail.com";
            var userLogin = new UserLogin
            {
                EmailOrUsername = email,
                Password = "ErrorError",
            };
            var UserResult = new DBUser
            {
                Password = "Testing123..."
            };

            var manager = new UserManager(_mockJWTManager.Object, _mockUserRepository.Object, _mockMailManager.Object, _mockOTPManager.Object);

            _mockUserRepository.Setup(x => x.FirstOrDefaultActiveUser(It.IsAny<string>())).ReturnsAsync(UserResult);

            async Task Act()
            {
                await manager.LoginAsync(userLogin);
            }

            Assert.ThrowsAsync<WrongEmailOrPasswordException>(Act);
        }

        [Test]
        public void LoginWithUserNotActive()
        {
            var email = "test@gmail.com";
            var userLogin = new UserLogin
            {
                EmailOrUsername = email,
                Password = "Testing123...",
            };
            var UserResult = new DBUser
            {
                Email = email,
                Password = "Testing123...",
                IsActive = false
            };

            var manager = new UserManager(_mockJWTManager.Object, _mockUserRepository.Object, _mockMailManager.Object, _mockOTPManager.Object);

            _mockUserRepository.Setup(x => x.FirstOrDefaultActiveUser(It.IsAny<string>())).ReturnsAsync(UserResult);
            _mockMailManager.Setup(x => x.SendRegisterOtpEmailAsync(email)).Returns(Task.CompletedTask);

            async Task Act()
            {
                await manager.LoginAsync(userLogin);
            }

            Assert.ThrowsAsync<AccountNotActivedException>(Act);
        }

        [Test]
        public void LoginSuccess()
        {
            var email = "test@gmail.com";
            var userLogin = new UserLogin
            {
                EmailOrUsername = email,
                Password = "Testing123...",
            };
            var UserResult = new DBUser
            {
                Password = "Testing123...",
                IsActive = true
            };

            var manager = new UserManager(_mockJWTManager.Object, _mockUserRepository.Object, _mockMailManager.Object, _mockOTPManager.Object);

            _mockUserRepository.Setup(x => x.FirstOrDefaultActiveUser(It.IsAny<string>())).ReturnsAsync(UserResult);
            _mockMailManager.Setup(x => x.SendRegisterOtpEmailAsync(email)).Returns(Task.CompletedTask);
            _mockJWTManager.Setup(x => x.JWTGenerate(It.IsAny<string>())).Returns("TestingJWT");

            async Task Act()
            {
                await manager.LoginAsync(userLogin);
            }

            Assert.DoesNotThrowAsync(Act);
        }

        [Test]
        public void VerifyOTP_Success()
        {
            var otp = "123456";
            var email = "test@gmail.com";

            _mockOTPManager.Setup(x => x.CheckOTP(email, otp)).Returns(Task.CompletedTask);
            _mockUserRepository.Setup(x => x.SetActiveUser(email)).Returns(Task.CompletedTask);
            _mockJWTManager.Setup(x => x.JWTGenerate(email)).Returns("ValidJWTToken");

            var manager = new UserManager(_mockJWTManager.Object, _mockUserRepository.Object, _mockMailManager.Object, _mockOTPManager.Object);

            async Task Act()
            {
                var result = await manager.VerifyOTP(otp, email);
            }

            Assert.DoesNotThrowAsync(Act);
        }

        [Test]
        public void VerifyOTP_InvalidOTP()
        {
            var otp = "invalid";
            var email = "test@gmail.com";

            _mockOTPManager.Setup(x => x.CheckOTP(email, otp)).ThrowsAsync(new WrongEmailOrOTPExeption());

            var manager = new UserManager(_mockJWTManager.Object, _mockUserRepository.Object, _mockMailManager.Object, _mockOTPManager.Object);

            async Task Act() => await manager.VerifyOTP(otp, email);

            Assert.ThrowsAsync<WrongEmailOrOTPExeption>(Act);
        }

        [Test]
        public void LoginOTP_Success()
        {
            var email = "test@gmail.com";

            _mockUserRepository.Setup(x => x.ExistActiveUser(email)).ReturnsAsync(true);
            _mockMailManager.Setup(x => x.SendLoginOtpEmailAsync(email)).Returns(Task.CompletedTask);

            var manager = new UserManager(_mockJWTManager.Object, _mockUserRepository.Object, _mockMailManager.Object, _mockOTPManager.Object);

            async Task Act() => await manager.LoginOTP(email);

            Assert.DoesNotThrowAsync(Act);
        }

        [Test]
        public void LoginOTP_EmailDoesNotExist()
        {
            var email = "nonexistent@gmail.com";

            _mockUserRepository.Setup(x => x.ExistActiveUser(email)).ReturnsAsync(false);

            var manager = new UserManager(_mockJWTManager.Object, _mockUserRepository.Object, _mockMailManager.Object, _mockOTPManager.Object);

            async Task Act() => await manager.LoginOTP(email);

            Assert.ThrowsAsync<NotExisitingEmailException>(Act);
        }

        [Test]
        public void ChangeUserPassword_Success()
        {
            var email = "test@gmail.com";
            var password = "ValidPass123..";

            _mockUserRepository.Setup(x => x.ChangeUserPassword(email, password)).Returns(Task.CompletedTask);

            var manager = new UserManager(_mockJWTManager.Object, _mockUserRepository.Object, _mockMailManager.Object, _mockOTPManager.Object);

            async Task Act() => await manager.ChangeUserPassword(email, password);

            Assert.DoesNotThrowAsync(Act);
        }

        [Test]
        public void ChangeUserPassword_InvalidPassword()
        {
            var email = "test@gmail.com";
            var password = "short";

            var manager = new UserManager(_mockJWTManager.Object, _mockUserRepository.Object, _mockMailManager.Object, _mockOTPManager.Object);

            async Task Act() => await manager.ChangeUserPassword(email, password);

            Assert.ThrowsAsync<MalformedDataException>(Act);
        }

        [Test]
        public void PasswordDimenticata_Success()
        {
            var email = "test@gmail.com";

            _mockUserRepository.Setup(x => x.ExistActiveUser(email)).ReturnsAsync(true);
            _mockMailManager.Setup(x => x.SendPasswordDimenticataEmailAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);
            _mockUserRepository.Setup(x => x.ChangeUserPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);

            var manager = new UserManager(_mockJWTManager.Object, _mockUserRepository.Object, _mockMailManager.Object, _mockOTPManager.Object);

            async Task Act() => await manager.PasswordDimenticata(email);

            Assert.DoesNotThrowAsync(Act);
        }

        [Test]
        public void PasswordDimenticata_EmailDoesNotExist()
        {
            var email = "nonexistent@gmail.com";

            _mockUserRepository.Setup(x => x.ExistActiveUser(email)).ReturnsAsync(false);

            var manager = new UserManager(_mockJWTManager.Object, _mockUserRepository.Object, _mockMailManager.Object, _mockOTPManager.Object);

            async Task Act() => await manager.PasswordDimenticata(email);

            Assert.ThrowsAsync<NotExisitingEmailException>(Act);
        }

        [Test]
        public async Task GetUserData_Success()
        {
            var email = "test@gmail.com";
            var user = new DBUser
            {
                Email = email,
                Username = "testuser"
            };

            _mockUserRepository.Setup(x => x.FirstOrDefaultActiveUser(email)).ReturnsAsync(user);

            var manager = new UserManager(_mockJWTManager.Object, _mockUserRepository.Object, _mockMailManager.Object, _mockOTPManager.Object);

            var result = await manager.GetUserData(email);

            Assert.AreEqual(email, result.Email);
            Assert.AreEqual("testuser", result.Username);
        }

        [Test]
        public void GetUserData_EmailDoesNotExist()
        {
            var email = "nonexistent@gmail.com";

            _mockUserRepository.Setup(x => x.FirstOrDefaultActiveUser(email)).ReturnsAsync((DBUser)null);

            var manager = new UserManager(_mockJWTManager.Object, _mockUserRepository.Object, _mockMailManager.Object, _mockOTPManager.Object);

            async Task Act() => await manager.GetUserData(email);

            Assert.ThrowsAsync<NotExisitingEmailException>(Act);
        }


    }
}