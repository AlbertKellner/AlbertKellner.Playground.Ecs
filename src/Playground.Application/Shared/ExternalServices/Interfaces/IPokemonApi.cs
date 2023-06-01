using Playground.Application.Shared.Domain.ApiDto;
using Refit;

namespace Playground.Application.Shared.ExternalServices.Interfaces
{
    public interface IPokemonApi
    {
        //[Get("/pokemon/{name}")]
        [Get("/pokemon/internal-name/{name}")]
        Task<PokemonOutApiDto> GetByNameAsync(string name, CancellationToken cancellationToken);
    }
}
