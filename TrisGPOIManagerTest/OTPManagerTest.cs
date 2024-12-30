using Moq;
using TrisGPOI.Core.OTP;
using TrisGPOI.Core.OTP.Exceptions;
using TrisGPOI.Core.OTP.Interfaces;

namespace TrisGPOIManagerTesting
{
    public class OTPManagerTest
    {
        private readonly Mock<IOTPRepository> _mockOTPRepository;
        private OTPManager _otpManager;
        public OTPManagerTest()
        {
            _mockOTPRepository = new Mock<IOTPRepository>(MockBehavior.Strict);
            _otpManager = new OTPManager(_mockOTPRepository.Object);
        }
        [Test]
        public void AddNewOTP_Success()
        {
            var email = "test1@gmail.com";
            var otp = "123456";

            _mockOTPRepository.Setup(x => x.AddNewOTP(email, otp)).Returns(Task.CompletedTask);

            async Task Act() => await _otpManager.AddNewOTP(email, otp);

            Assert.DoesNotThrowAsync(Act);
            _mockOTPRepository.Verify(x => x.AddNewOTP(email, otp), Times.Once);
        }

        [Test]
        public void AddNewOTP_RepositoryThrowsException()
        {
            var email = "test2@gmail.com";
            var otp = "123456";

            _mockOTPRepository.Setup(x => x.AddNewOTP(email, otp)).ThrowsAsync(new Exception("Database error"));

            async Task Act() => await _otpManager.AddNewOTP(email, otp);

            Assert.ThrowsAsync<Exception>(Act);
            _mockOTPRepository.Verify(x => x.AddNewOTP(email, otp), Times.Once);
        }
        [Test]
        public void GenerateOtp_ReturnsSixDigitCode()
        {
            var otp = _otpManager.GenerateOtp();

            Assert.That(otp, Has.Length.EqualTo(6));
            Assert.That(int.TryParse(otp, out _), Is.True);
        }
        [Test]
        public async Task CheckOTP_Success()
        {
            var email = "test3@gmail.com";
            var otp = "123456";

            _mockOTPRepository.Setup(x => x.CheckOTP(email, otp)).ReturnsAsync(true);

            async Task Act() => await _otpManager.CheckOTP(email, otp);

            Assert.DoesNotThrowAsync(Act);
            _mockOTPRepository.Verify(x => x.CheckOTP(email, otp), Times.Once);
        }

        [Test]
        public void CheckOTP_WrongOTP_ThrowsWrongEmailOrOTPException()
        {
            var email = "test4@gmail.com";
            var otp = "654321";

            _mockOTPRepository.Setup(x => x.CheckOTP(email, otp)).ReturnsAsync(false);

            async Task Act() => await _otpManager.CheckOTP(email, otp);

            Assert.ThrowsAsync<WrongEmailOrOTPExeption>(Act);
            _mockOTPRepository.Verify(x => x.CheckOTP(email, otp), Times.Once);
        }

        [Test]
        public void CheckOTP_RepositoryThrowsException()
        {
            var email = "test5@gmail.com";
            var otp = "123456";

            _mockOTPRepository.Setup(x => x.CheckOTP(email, otp)).ThrowsAsync(new Exception("Database error"));

            async Task Act() => await _otpManager.CheckOTP(email, otp);

            Assert.ThrowsAsync<Exception>(Act);
            _mockOTPRepository.Verify(x => x.CheckOTP(email, otp), Times.Once);
        }

    }
}
