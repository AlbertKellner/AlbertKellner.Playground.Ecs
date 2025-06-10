using Moq;
using Microsoft.Extensions.Logging;
using Playground.Application.Features.Pokemon.GetByName.UseCase;
using Playground.Application.Features.Pokemon.GetByName.Models;
using Playground.Application.Shared.ExternalServices.Interfaces;
using Playground.Application.Shared.Domain.ApiDto;

namespace Playground.Tests.Controllers
{
    public class HandleGetByNamePokemonUseCaseHandlerTest
    {
        private readonly Mock<IPokemonApi> _mockPokemonApi;
        private readonly Mock<ILogger<GetByNamePokemonUseCaseHandler>> _mockLogger;
        private readonly GetByNamePokemonUseCaseHandler _handler;

        public HandleGetByNamePokemonUseCaseHandlerTest()
        {
            _mockPokemonApi = new Mock<IPokemonApi>();
            _mockLogger = new Mock<ILogger<GetByNamePokemonUseCaseHandler>>();
            _handler = new GetByNamePokemonUseCaseHandler(_mockPokemonApi.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_QuandoExecutado_DeveRetornarPokemon()
        {
            var query = new GetByNamePokemonQuery();
            query.SetName("pikachu");

            var apiDto = new PokemonOutApiDto
            {
                Name = "pikachu",
                BaseExperience = 10,
                LocationAreaEncounters = "url"
            };

            _mockPokemonApi
                .Setup(api => api.GetByNameAsync("pikachu", It.IsAny<CancellationToken>()))
                .ReturnsAsync(apiDto);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Equal(apiDto.Name, result.Name);
            Assert.Equal(apiDto.BaseExperience, result.BaseExperience);
            Assert.Equal(apiDto.LocationAreaEncounters, result.LocationAreaEncounters);
            _mockPokemonApi.Verify(api => api.GetByNameAsync("pikachu", It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
