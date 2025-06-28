using Microsoft.AspNetCore.Http;
using MediatR;
using Microsoft.Extensions.Logging;
using Playground.Application.Features.Pokemon.GetByName.Models;

namespace Playground.Application.Features.Pokemon.GetByName.Interface;

public interface IGetByNameExternalEndpoint
{
    Task<IResult> HandleAsync(
        string name,
        GetByNamePokemonQuery input,
        IMediator mediator,
        ILoggerFactory loggerFactory,
        CancellationToken cancellationToken);
}
