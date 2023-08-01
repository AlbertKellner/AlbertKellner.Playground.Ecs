using MediatR;
using Microsoft.Extensions.Logging;
using Playground.Application.Features.Pokemon.GetByName.Models;
using Playground.Application.Shared.ExternalServices.Interfaces;

namespace Playground.Application.Features.Pokemon.GetByName.UseCase
{
    public class GetByNamePokemonUseCaseHandler : IRequestHandler<GetByNamePokemonQuery, GetByNamePokemonOutput>
    {
        private readonly IPokemonApi _pokemonApi;
        private readonly ILogger<GetByNamePokemonUseCaseHandler> _logger;

        public GetByNamePokemonUseCaseHandler(
            IPokemonApi pokemonApi,
            ILogger<GetByNamePokemonUseCaseHandler> logger)
        {
            _pokemonApi = pokemonApi;
            _logger = logger;
        }

        public async Task<GetByNamePokemonOutput> Handle(GetByNamePokemonQuery input, CancellationToken cancellationToken)
        {
            var pokemonApiDto = await _pokemonApi.GetByNameAsync(input.Name, cancellationToken);

             var output = new GetByNamePokemonOutput
            {
                Name = pokemonApiDto.Name,
                BaseExperience = pokemonApiDto.BaseExperience,
                LocationAreaEncounters = pokemonApiDto.LocationAreaEncounters
            };

            _logger.LogInformation("[Features][GetByNamePokemonUseCaseHandler][Handle][Ok] input:({@input})", input.ToInformation());

            return output;
        }
    }
}
