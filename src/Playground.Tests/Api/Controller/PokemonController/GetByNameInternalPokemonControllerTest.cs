using Moq;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Playground.Controllers;
using Playground.Application.Shared.Domain.ApiDto;

namespace Playground.Tests.Controllers
{
    public class GetByNameInternalPokemonControllerTest
    {
        private readonly Mock<ILogger<PokemonController>> _mockLogger;
        private readonly PokemonController _controller;

        public GetByNameInternalPokemonControllerTest()
        {
            _mockLogger = new Mock<ILogger<PokemonController>>();
            _controller = new PokemonController(Mock.Of<IMediator>(), _mockLogger.Object);
        }

        [Fact]
        public void GetByNameInternalAsync_QuandoPikachu_DeveRetornarOk()
        {
            var result = _controller.GetByNameInternalAsync("pikachu");

            var response = Assert.IsType<OkObjectResult>(result);
            var pokemon = Assert.IsType<PokemonOutApiDto>(response.Value);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.Equal("pikachu", pokemon.Name);
        }

        [Fact]
        public void GetByNameInternalAsync_QuandoNaoForPikachu_DeveRetornarNoContent()
        {
            var result = _controller.GetByNameInternalAsync("mew");

            var response = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, response.StatusCode);
        }
    }
}
