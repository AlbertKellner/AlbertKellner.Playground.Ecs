using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Playground.Controllers;

namespace Playground.Tests.Controllers
{
    public class GenerateTokenAuthControllerTest
    {
        private readonly Mock<ILogger<AuthController>> _mockLogger;
        private readonly AuthController _controller;

        public GenerateTokenAuthControllerTest()
        {
            _mockLogger = new Mock<ILogger<AuthController>>();
            _controller = new AuthController(_mockLogger.Object);
        }

        [Fact]
        public void GenerateToken_DeveRetornarOkComToken()
        {
            var user = new AuthController.AuthUser("1", "tester", "group");

            var actionResult = _controller.GenerateToken(user);

            var response = Assert.IsType<OkObjectResult>(actionResult);

            Assert.NotNull(response.Value);
            var tokenProperty = response.Value!.GetType().GetProperty("Token");
            Assert.NotNull(tokenProperty);
            var token = tokenProperty!.GetValue(response.Value) as string;
            Assert.False(string.IsNullOrEmpty(token));
        }
    }
}
