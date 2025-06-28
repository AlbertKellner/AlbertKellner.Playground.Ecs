using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Playground.Application.Shared.Domain.ApiDto;

namespace Playground.Application.Features.Pokemon.Endpoint;

public static class GetByNameInternalEndpoint
{
    public static IEndpointRouteBuilder MapGetByNameInternalEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/pokemon/internal-name/{name}", (
            string name,
            ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger("GetByNameInternalEndpoint");
            if (name == "pikachu")
            {
                logger.LogInformation("[Pokemon][GetByNameInternalEndpoint] Retorno do endpoint internal com sucesso. input:({@pokemonName})", name);

                return Results.Ok(new PokemonOutApiDto
                {
                    Name = "pikachu",
                    BaseExperience = 112,
                    LocationAreaEncounters = "Grass"
                });
            }

            logger.LogInformation("[Pokemon][GetByNameInternalEndpoint] Retorno do endpoint internal sem dados. input:({@pokemonName})", name);

            return Results.NoContent();
        })
        .Produces<PokemonOutApiDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status204NoContent);

        return endpoints;
    }
}
