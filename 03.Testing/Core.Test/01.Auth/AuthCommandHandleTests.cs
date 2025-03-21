using Auth.VirtualCare.Application.Modules.Auth.Commands;
using Auth.VirtualCare.Domain.Interfaces.Auth;
using Moq;
using Shared._01.Auth.DTOs;

namespace Auth.VirtualCare.Tests
{
    public class AuthCommandHandleTests
    {
        private readonly Mock<IAuthRepository> _authRepositoryMock;
        private readonly AuthCommandHandle _handler;

        public AuthCommandHandleTests()
        {
            _authRepositoryMock = new Mock<IAuthRepository>();
            _handler = new AuthCommandHandle(_authRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessOperation_WhenLoginIsSuccessful()
        {
            // Arrange
            var command = new LoginCommand("testuser", "password");
            var loginResult = new LoginResultDTO
            {
                Success = true,
                Token = "testtoken",
                Message = "Login successful"
            };

            _authRepositoryMock.Setup(repo => repo.Login(command.userName, command.password))
                .ReturnsAsync(loginResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("testtoken", result.Data);
            Assert.Equal("Login successful", result.Message);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResultNoRecords_WhenLoginFails()
        {
            // Arrange
            var command = new LoginCommand("testuser", "wrongpassword");
            var loginResult = new LoginResultDTO
            {
                Success = false,
                Message = "Login failed"
            };

            _authRepositoryMock.Setup(repo => repo.Login(command.userName, command.password))
                .ReturnsAsync(loginResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Login failed", result.Message);
            Assert.Null(result.Data);
        }
    }
}
