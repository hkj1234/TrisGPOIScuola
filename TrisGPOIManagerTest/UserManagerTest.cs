using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using Moq;
using TrisGPOI.Core.Home.Interfaces;
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
        private readonly Mock<IUserVittorieRepository> _mockUserVittorieRepository;
        private readonly Mock<IHomeManager> _mockHomeManager;
        private readonly UserManager _userManager;
        public UserManagerTest()
        {
            _mockJWTManager = new Mock<IJWTManager>(MockBehavior.Strict);
            _mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            _mockMailManager = new Mock<IMailManager>(MockBehavior.Strict);
            _mockOTPManager = new Mock<IOTPManager>(MockBehavior.Strict);
            _mockUserVittorieRepository = new Mock<IUserVittorieRepository>(MockBehavior.Strict);
            _mockHomeManager = new Mock<IHomeManager>(MockBehavior.Strict);
            _userManager = new UserManager(_mockJWTManager.Object, _mockUserRepository.Object, _mockMailManager.Object, _mockOTPManager.Object, _mockUserVittorieRepository.Object,
                _mockHomeManager.Object);
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

            _mockUserRepository.Setup(x => x.ExistActiveUser(email)).ReturnsAsync(false);
            _mockUserRepository.Setup(x => x.ExistActiveUser(username)).ReturnsAsync(true);

            async Task Act()
            {
                await _userManager.RegisterAsync(userRegister);
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

            _mockUserRepository.Setup(x => x.ExistActiveUser(email)).ReturnsAsync(true);
            _mockUserRepository.Setup(x => x.ExistActiveUser(username)).ReturnsAsync(false);

            async Task Act()
            {
                await _userManager.RegisterAsync(userRegister);
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

            _mockUserRepository.Setup(x => x.ExistActiveUser(email)).ReturnsAsync(false);
            _mockUserRepository.Setup(x => x.ExistActiveUser(username)).ReturnsAsync(false);

            async Task Act()
            {
                await _userManager.RegisterAsync(userRegister);
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

            _mockUserRepository.Setup(x => x.ExistActiveUser(email)).ReturnsAsync(false);
            _mockUserRepository.Setup(x => x.ExistActiveUser(username)).ReturnsAsync(false);

            async Task Act()
            {
                await _userManager.RegisterAsync(userRegister);
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

            _mockUserRepository.Setup(x => x.ExistActiveUser(email)).ReturnsAsync(false);
            _mockUserRepository.Setup(x => x.ExistActiveUser(username)).ReturnsAsync(false);

            async Task Act()
            {
                await _userManager.RegisterAsync(userRegister);
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

            _mockUserRepository.Setup(x => x.ExistActiveUser(email)).ReturnsAsync(false);
            _mockUserRepository.Setup(x => x.ExistActiveUser(username)).ReturnsAsync(false);
            _mockMailManager.Setup(x => x.SendRegisterOtpEmailAsync(email)).Returns(Task.CompletedTask);
            _mockUserRepository.Setup(x => x.ExistUser(email)).ReturnsAsync(false);
            _mockUserRepository.Setup(x => x.AddNewUserAsync(userRegister)).Returns(Task.CompletedTask);


            async Task Act()
            {
                await _userManager.RegisterAsync(userRegister);
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

            _mockUserRepository.Setup(x => x.ExistActiveUser(email)).ReturnsAsync(false);
            _mockUserRepository.Setup(x => x.ExistActiveUser(username)).ReturnsAsync(false);
            _mockMailManager.Setup(x => x.SendRegisterOtpEmailAsync(email)).Returns(Task.CompletedTask);
            _mockUserRepository.Setup(x => x.ExistUser(email)).ReturnsAsync(true);

            async Task Act()
            {
                await _userManager.RegisterAsync(userRegister);
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


            _mockUserRepository.Setup(x => x.FirstOrDefaultActiveUser(It.IsAny<string>())).ReturnsAsync(UserResult);

            async Task Act()
            {
                await _userManager.LoginAsync(userLogin);
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

            _mockUserRepository.Setup(x => x.FirstOrDefaultActiveUser(It.IsAny<string>())).ReturnsAsync(UserResult);

            async Task Act()
            {
                await _userManager.LoginAsync(userLogin);
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


            _mockUserRepository.Setup(x => x.FirstOrDefaultActiveUser(It.IsAny<string>())).ReturnsAsync(UserResult);
            _mockMailManager.Setup(x => x.SendRegisterOtpEmailAsync(email)).Returns(Task.CompletedTask);

            async Task Act()
            {
                await _userManager.LoginAsync(userLogin);
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

            _mockUserRepository.Setup(x => x.FirstOrDefaultActiveUser(It.IsAny<string>())).ReturnsAsync(UserResult);
            _mockMailManager.Setup(x => x.SendRegisterOtpEmailAsync(email)).Returns(Task.CompletedTask);
            _mockJWTManager.Setup(x => x.JWTGenerate(It.IsAny<string>())).Returns("TestingJWT");

            async Task Act()
            {
                await _userManager.LoginAsync(userLogin);
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

            async Task Act()
            {
                var result = await _userManager.VerifyOTP(otp, email);
            }

            Assert.DoesNotThrowAsync(Act);
        }

        [Test]
        public void VerifyOTP_InvalidOTP()
        {
            var otp = "invalid";
            var email = "test@gmail.com";

            _mockOTPManager.Setup(x => x.CheckOTP(email, otp)).ThrowsAsync(new WrongEmailOrOTPExeption());

            async Task Act() => await _userManager.VerifyOTP(otp, email);

            Assert.ThrowsAsync<WrongEmailOrOTPExeption>(Act);
        }

        [Test]
        public void LoginOTP_Success()
        {
            var email = "test@gmail.com";

            _mockUserRepository.Setup(x => x.ExistActiveUser(email)).ReturnsAsync(true);
            _mockMailManager.Setup(x => x.SendLoginOtpEmailAsync(email)).Returns(Task.CompletedTask);

            async Task Act() => await _userManager.LoginOTP(email);

            Assert.DoesNotThrowAsync(Act);
        }

        [Test]
        public void LoginOTP_EmailDoesNotExist()
        {
            var email = "nonexistent@gmail.com";

            _mockUserRepository.Setup(x => x.ExistActiveUser(email)).ReturnsAsync(false);

            async Task Act() => await _userManager.LoginOTP(email);

            Assert.ThrowsAsync<NotExisitingEmailException>(Act);
        }

        [Test]
        public void ChangeUserPassword_Success()
        {
            var email = "test@gmail.com";
            var password = "ValidPass123..";

            _mockUserRepository.Setup(x => x.ChangeUserPassword(email, password)).Returns(Task.CompletedTask);

            async Task Act() => await _userManager.ChangeUserPassword(email, password);

            Assert.DoesNotThrowAsync(Act);
        }

        [Test]
        public void ChangeUserPassword_InvalidPassword()
        {
            var email = "test@gmail.com";
            var password = "short";

            async Task Act() => await _userManager.ChangeUserPassword(email, password);

            Assert.ThrowsAsync<MalformedDataException>(Act);
        }

        [Test]
        public void PasswordDimenticata_Success()
        {
            var email = "test@gmail.com";

            _mockUserRepository.Setup(x => x.ExistActiveUser(email)).ReturnsAsync(true);
            _mockMailManager.Setup(x => x.SendPasswordDimenticataEmailAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);
            _mockUserRepository.Setup(x => x.ChangeUserPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask);

            async Task Act() => await _userManager.PasswordDimenticata(email);

            Assert.DoesNotThrowAsync(Act);
        }

        [Test]
        public void PasswordDimenticata_EmailDoesNotExist()
        {
            var email = "nonexistent@gmail.com";

            _mockUserRepository.Setup(x => x.ExistActiveUser(email)).ReturnsAsync(false);

            async Task Act() => await _userManager.PasswordDimenticata(email);

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
            var userVictory = new DBUserVittoriePVP
            {

            };

            _mockUserVittorieRepository.Setup(x => x.FindVittorieWithEmail(email)).ReturnsAsync(userVictory);
            _mockUserRepository.Setup(x => x.FirstOrDefaultActiveUser(email)).ReturnsAsync(user);

            var result = await _userManager.GetUserData(email);

            Assert.AreEqual(email, result.Email);
            Assert.AreEqual("testuser", result.Username);
        }

        [Test]
        public void GetUserData_EmailDoesNotExist()
        {
            var email = "nonexistent@gmail.com";

            _mockUserRepository.Setup(x => x.FirstOrDefaultActiveUser(email)).ReturnsAsync((DBUser)null);

            async Task Act() => await _userManager.GetUserData(email);

            Assert.ThrowsAsync<NotExisitingEmailException>(Act);
        }

        [Test]
        public async Task ChangeUserDescription_ValidInput_CallsRepositoryMethod()
        {
            // Arrange
            string email = "test@example.com";
            string description = "New description";

            _mockUserRepository
                .Setup(repo => repo.ChangeUserDescription(email, description))
                .Returns(Task.CompletedTask);

            // Act
            await _userManager.ChangeUserDescription(email, description);

            // Assert
            _mockUserRepository.Verify(repo => repo.ChangeUserDescription(email, description), Times.Once);
        }

        [Test]
        public async Task ChangeUserFoto_ValidFoto_CallsRepositoryMethod()
        {
            // Arrange
            string email = "test@example.com";
            string foto = "Collezionabile1";

            _mockUserRepository
                .Setup(repo => repo.ChangeUserFoto(email, foto))
                .Returns(Task.CompletedTask);

            // Act
            await _userManager.ChangeUserFoto(email, foto);

            // Assert
            _mockUserRepository.Verify(repo => repo.ChangeUserFoto(email, foto), Times.Once);
        }

        [Test]
        public void ChangeUserFoto_InvalidFoto_ThrowsMalformedDataException()
        {
            // Arrange
            string email = "test@example.com";
            string foto = "invalidImage";

            // Act & Assert
            Assert.ThrowsAsync<MalformedDataException>(async () =>
            {
                await _userManager.ChangeUserFoto(email, foto);
            });

            _mockUserRepository.Verify(repo => repo.ChangeUserFoto(email, foto), Times.Never);
        }

        [Test]
        public async Task ChangeUserStatus_ValidStatus_CallsRepositoryMethod()
        {
            // Arrange
            string email = "test@example.com";
            string status = "Online"; // Un valore valido
            _mockUserRepository
                .Setup(repo => repo.ChangeUserStatus(email, status))
                .Returns(Task.CompletedTask);

            _mockHomeManager.Setup(x => x.ChangeUserStatus(email, status)).Returns(Task.CompletedTask);

            // Act
            await _userManager.ChangeUserStatus(email, status);

            // Assert
            _mockHomeManager.Verify(repo => repo.ChangeUserStatus(email, status), Times.Once);
        }
    }
}