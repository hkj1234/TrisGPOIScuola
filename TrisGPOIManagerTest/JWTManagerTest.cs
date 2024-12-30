using Moq;
using TrisGPOI.Core.JWT;
using TrisGPOI.Core.JWT.Entities;
using TrisGPOI.Core.JWT.Interfaces;

namespace TrisGPOIManagerTesting
{
    [TestFixture]
    public class JWTManagerTest
    {
        private Mock<IGetOptionManager> _mockGetOptionManager;
        private JWTManager _jwtManager;

        [SetUp]
        public void SetUp()
        {
            _mockGetOptionManager = new Mock<IGetOptionManager>(MockBehavior.Strict);
            _jwtManager = new JWTManager(_mockGetOptionManager.Object);
        }

        [Test]
        public void JWTGenerate_ValidData_ReturnsToken()
        {
            // Configura le opzioni simulate
            var tokenOptions = new TokenOptions
            {
                Secret = "SuperSecretKeyForJWTGeneration123",
                Issuer = "TestIssuer",
                Audience = "TestAudience",
                ExpiryDays = 7
            };

            _mockGetOptionManager.Setup(x => x.GetTokenOptions()).Returns(tokenOptions);

            // Act
            var result = _jwtManager.JWTGenerate("TestUser");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);

            // Verifica che il metodo GetTokenOptions sia stato chiamato
            _mockGetOptionManager.Verify(x => x.GetTokenOptions(), Times.Once);
        }

        [Test]
        public void JWTGenerate_NullOrEmptyData_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _jwtManager.JWTGenerate(null));
            Assert.Throws<ArgumentNullException>(() => _jwtManager.JWTGenerate(string.Empty));
        }

        [Test]
        public void JWTGenerate_InvalidOptions_ThrowsException()
        {
            // Configura opzioni nulle
            _mockGetOptionManager.Setup(x => x.GetTokenOptions()).Returns((TokenOptions)null);

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => _jwtManager.JWTGenerate("TestUser"));
            _mockGetOptionManager.Verify(x => x.GetTokenOptions(), Times.Once);
        }

        [Test]
        public void JWTGenerate_ShortSecret_ThrowsArgumentException()
        {
            // Configura un secret troppo corto
            var tokenOptions = new TokenOptions
            {
                Secret = "Short",
                Issuer = "TestIssuer",
                Audience = "TestAudience",
                ExpiryDays = 7
            };

            _mockGetOptionManager.Setup(x => x.GetTokenOptions()).Returns(tokenOptions);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _jwtManager.JWTGenerate("TestUser"));
            _mockGetOptionManager.Verify(x => x.GetTokenOptions(), Times.Once);
        }
    }
}
