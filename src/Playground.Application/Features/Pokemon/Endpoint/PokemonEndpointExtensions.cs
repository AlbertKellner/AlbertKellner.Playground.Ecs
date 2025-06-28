using Microsoft.AspNetCore.Routing;
using Playground.Application.Features.Pokemon.GetByName.Endpoint;

namespace Playground.Application.Features.Pokemon.Endpoint;

public static class PokemonEndpointExtensions
{
    public static IEndpointRouteBuilder MapPokemonEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGetByNameExternalEndpoint();
        endpoints.MapGetByNameInternalEndpoint();
        return endpoints;
    }
}
