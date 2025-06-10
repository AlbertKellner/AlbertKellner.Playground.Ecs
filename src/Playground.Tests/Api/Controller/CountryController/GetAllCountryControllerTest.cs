using Moq;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Playground.Controllers;
using Playground.Application.Features.Country.Query.GetAll.Models;

namespace Playground.Tests.Controllers
{
    public class GetAllCountryControllerTest
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<CountryController>> _mockLogger;
        private readonly CountryController _controller;

        public GetAllCountryControllerTest()
        {
            _mockMediator = new Mock<IMediator>();
            _mockLogger = new Mock<ILogger<CountryController>>();
            _controller = new CountryController(_mockMediator.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllAsync_QuandoExistemPaises_DeveRetornarOk()
        {
            var output = new List<GetAllCountryOutput>
            {
                new GetAllCountryOutput { Name = "Brazil" }
            };

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetAllCountryQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(output);

            var result = await _controller.GetAllAsync(CancellationToken.None);

            var response = Assert.IsType<OkObjectResult>(result);
            var responseData = Assert.IsType<List<GetAllCountryOutput>>(response.Value);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.Equal(output, responseData);
        }

        [Fact]
        public async Task GetAllAsync_QuandoNaoExistemPaises_DeveRetornarNoContent()
        {
            var output = new List<GetAllCountryOutput>();

            _mockMediator
                .Setup(m => m.Send(It.IsAny<GetAllCountryQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(output);

            var result = await _controller.GetAllAsync(CancellationToken.None);

            var response = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, response.StatusCode);
        }
    }
}
