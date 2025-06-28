using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Playground.Application.Features.Pokemon.Interface;

public interface IGetByNameInternalEndpoint
{
    IResult Handle(string name, ILoggerFactory loggerFactory);
}
