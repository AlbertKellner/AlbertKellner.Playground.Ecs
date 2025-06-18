using Moq;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Playground.API.Controllers;
using Playground.Application.Features.Pokemon.GetByName.Models;

namespace Playground.Tests.Controllers
{
    public class GetByNameExternalPokemonControllerTest
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<PokemonController>> _mockLogger;
        private readonly PokemonController _controller;
        private readonly GetByNamePokemonQuery _validInput;
        private readonly GetByNamePokemonQuery _invalidInput;
        private readonly GetByNamePokemonOutput _validOutput;
        private readonly GetByNamePokemonOutput _invalidOutput;

        public GetByNameExternalPokemonControllerTest()
        {
            _mockMediator = new Mock<IMediator>();
            _mockLogger = new Mock<ILogger<PokemonController>>();
            _controller = new PokemonController(_mockMediator.Object, _mockLogger.Object);

            _validInput = new GetByNamePokemonQuery();
            _validInput.SetName("pikachu");

            _invalidInput = new GetByNamePokemonQuery();
            _invalidInput.SetName(string.Empty);

            _validOutput = new GetByNamePokemonOutput
            {
                Name = "pikachu",
                BaseExperience = 100,
                LocationAreaEncounters = "Forest"
            };

            _invalidOutput = new GetByNamePokemonOutput();
        }

        [Fact(DisplayName = "GetByNameExternalAsync QuandoValido DeveRetornarOk")]
        public async Task GetByNameExternalAsync_QuandoValido_DeveRetornarOk()
        {
            _mockMediator
                .Setup(m => m.Send(_validInput, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_validOutput);

            var result = await _controller.GetByNameExternalAsync(_validInput.Name, _validInput, CancellationToken.None);

            var response = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.Equal(_validOutput, response.Value);
            _mockMediator.Verify(m =>
                m.Send(_validInput, It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact(DisplayName = "GetByNameExternalAsync QuandoEntradaInvalida DeveRetornarBadRequest")]
        public async Task GetByNameExternalAsync_QuandoEntradaInvalida_DeveRetornarBadRequest()
        {
            _mockMediator
                .Setup(m => m.Send(_invalidInput, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_invalidOutput);

            var result = await _controller.GetByNameExternalAsync(_invalidInput.Name, _invalidInput, CancellationToken.None);

            var response = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, response.StatusCode);
            Assert.NotNull(response.Value);
            _mockMediator.Verify(m =>
                m.Send(It.IsAny<GetByNamePokemonQuery>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact(DisplayName = "GetByNameExternalAsync QuandoOutputInvalido DeveRetornarNoContent")]
        public async Task GetByNameExternalAsync_QuandoOutputInvalido_DeveRetornarNoContent()
        {
            _mockMediator
                .Setup(m => m.Send(_validInput, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_invalidOutput);

            var result = await _controller.GetByNameExternalAsync(_validInput.Name, _validInput, CancellationToken.None);

            var response = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, response.StatusCode);
            _mockMediator.Verify(m =>
                m.Send(_validInput, It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
