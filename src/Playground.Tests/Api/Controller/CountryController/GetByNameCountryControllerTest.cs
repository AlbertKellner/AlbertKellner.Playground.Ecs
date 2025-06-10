using Moq;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Playground.Controllers;
using Playground.Application.Features.Country.Query.GetByName.Models;

namespace Playground.Tests.Controllers
{
    public class GetByNameCountryControllerTest
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<CountryController>> _mockLogger;
        private readonly CountryController _controller;
        private readonly GetByNameCountryQuery _validInput;
        private readonly GetByNameCountryQuery _invalidInput;
        private readonly GetByNameCountryOutput _validOutput;
        private readonly GetByNameCountryOutput _invalidOutput;

        public GetByNameCountryControllerTest()
        {
            _mockMediator = new Mock<IMediator>();
            _mockLogger = new Mock<ILogger<CountryController>>();
            _controller = new CountryController(_mockMediator.Object, _mockLogger.Object);

            _validInput = new GetByNameCountryQuery();
            _validInput.SetName("brazil");

            _invalidInput = new GetByNameCountryQuery();
            _invalidInput.SetName(string.Empty);

            _validOutput = new GetByNameCountryOutput { Name = "Brazil" };
            _invalidOutput = new GetByNameCountryOutput { Name = string.Empty };
        }

        [Fact]
        public async Task GetByNameAsync_QuandoEntradaValida_DeveRetornarOk()
        {
            _mockMediator
                .Setup(m => m.Send(_validInput, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_validOutput);

            var result = await _controller.GetByNameAsync(_validInput.Name, _validInput, CancellationToken.None);

            var response = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.Equal(_validOutput, response.Value);
            _mockMediator.Verify(m =>
                m.Send(_validInput, It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task GetByNameAsync_QuandoEntradaInvalida_DeveRetornarBadRequest()
        {
            _mockMediator
                .Setup(m => m.Send(_invalidInput, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_invalidOutput);

            var result = await _controller.GetByNameAsync(_invalidInput.Name, _invalidInput, CancellationToken.None);

            var response = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, response.StatusCode);
            Assert.NotNull(response.Value);
            _mockMediator.Verify(m =>
                m.Send(It.IsAny<GetByNameCountryQuery>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task GetByNameAsync_QuandoOutputInvalido_DeveRetornarNoContent()
        {
            _mockMediator
                .Setup(m => m.Send(_validInput, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_invalidOutput);

            var result = await _controller.GetByNameAsync(_validInput.Name, _validInput, CancellationToken.None);

            var response = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, response.StatusCode);
            _mockMediator.Verify(m =>
                m.Send(_validInput, It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
