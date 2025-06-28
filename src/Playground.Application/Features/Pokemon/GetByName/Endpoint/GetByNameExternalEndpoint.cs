using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Playground.Application.Features.Pokemon.GetByName.Models;

namespace Playground.Application.Features.Pokemon.GetByName.Endpoint;

public static class GetByNameExternalEndpoint
{
    public static IEndpointRouteBuilder MapGetByNameExternalEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/pokemon/external-name/{name}", async (
            string name,
            [AsParameters] GetByNamePokemonQuery input,
            IMediator mediator,
            ILoggerFactory loggerFactory,
            CancellationToken cancellationToken) =>
        {
            var logger = loggerFactory.CreateLogger("GetByNameExternalEndpoint");
            input.SetName(name);

            if (input.IsInvalid())
            {
                logger.LogWarning("[Pokemon][GetByNameExternalEndpoint] Retornando API com erro de valida\u00E7\u00E3o. input:({@input})", input.ToWarning());
                return Results.BadRequest(input.ErrosList());
            }

            logger.LogInformation("[Pokemon][GetByNameExternalEndpoint] Iniciando caso de uso. input:({@input})", input.ToInformation());

            var output = await mediator.Send(input, cancellationToken);

            if (output.IsValid())
            {
                logger.LogInformation("[Pokemon][GetByNameExternalEndpoint] Retornando API com sucesso. input:({@input})", input.ToInformation());
                return Results.Ok(output);
            }

            logger.LogInformation("[Pokemon][GetByNameExternalEndpoint] Retornando API sem dados. input:({@input})", input.ToInformation());

            return Results.NoContent();
        })
        .Produces<GetByNamePokemonOutput>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest);

        return endpoints;
    }
}
