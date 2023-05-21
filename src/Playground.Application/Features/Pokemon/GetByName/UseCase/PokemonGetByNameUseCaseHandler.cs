using MediatR;
using Microsoft.Extensions.Logging;
using Playground.Application.Features.Pokemon.GetByName.Models;
using Playground.Application.Shared.ExternalServices.Interfaces;

namespace Playground.Application.Features.Pokemon.GetByName.UseCase
{
    public class PokemonGetByNameUseCaseHandler : IRequestHandler<PokemonGetByNameInput, PokemonGetByNameOutput>
    {
        private readonly IPokemonApi _pokemonApi;
        private readonly ILogger<PokemonGetByNameUseCaseHandler> _logger;

        public PokemonGetByNameUseCaseHandler(
            IPokemonApi pokemonApi,
            ILogger<PokemonGetByNameUseCaseHandler> logger)
        {
            _pokemonApi = pokemonApi;
            _logger = logger;
        }

        public async Task<PokemonGetByNameOutput> Handle(PokemonGetByNameInput input, CancellationToken cancellationToken)
        {
            var pokemonApiDto = await _pokemonApi.GetByNameAsync(input.Name, cancellationToken);

             var output = new PokemonGetByNameOutput
            {
                Name = pokemonApiDto.Name
            };

            _logger.LogInformation($"[Features][PokemonGetByNameUseCaseHandler][Handle][Ok] input:({input.ToInformation()})");

            return output;
        }
    }
}
