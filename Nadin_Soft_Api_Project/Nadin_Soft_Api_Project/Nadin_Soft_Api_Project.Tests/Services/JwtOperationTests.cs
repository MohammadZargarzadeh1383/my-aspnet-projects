using Microsoft.Extensions.Configuration;
using Nadin_Soft_Api_Project.Application.Services;
using Nadin_Soft_Api_Project.Domain.Entities.User;
using Xunit;

namespace Nadin_Soft_Api_Project.Tests.Services
{
    public class JwtOperationTests
    {
        private readonly JwtOperation _jwtOperation;
        private readonly IConfiguration _configuration;

        public JwtOperationTests()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"Jwt:Key", "YourTestSecretKey123!@#"},
                {"Jwt:Issuer", "TestIssuer"},
                {"Jwt:Audience", "TestAudience"}
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _jwtOperation = new JwtOperation(_configuration);
        }

        [Fact]
        public void GenerateToken_ShouldReturnValidToken()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = "testuser",
                Email = "test@example.com"
            };

            // Act
            var token = _jwtOperation.GenerateToken(user);

            // Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }

        [Fact]
        public void GenerateToken_ShouldContainUserClaims()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = "testuser",
                Email = "test@example.com"
            };

            // Act
            var token = _jwtOperation.GenerateToken(user);

            // Assert
            var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            Assert.Contains(jwtToken.Claims, claim => claim.Type == "nameid" && claim.Value == user.Id.ToString());
            Assert.Contains(jwtToken.Claims, claim => claim.Type == "unique_name" && claim.Value == user.UserName);
            Assert.Contains(jwtToken.Claims, claim => claim.Type == "email" && claim.Value == user.Email);
        }

        [Fact]
        public void GenerateToken_ShouldHaveValidExpiration()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = "testuser",
                Email = "test@example.com"
            };

            // Act
            var token = _jwtOperation.GenerateToken(user);

            // Assert
            var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            Assert.True(jwtToken.ValidTo > DateTime.UtcNow);
            Assert.True(jwtToken.ValidTo <= DateTime.UtcNow.AddHours(1)); // Assuming 1 hour expiration
        }
    }
} 