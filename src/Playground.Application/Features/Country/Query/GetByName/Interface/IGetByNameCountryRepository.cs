using Playground.Application.Features.Country.Query.GetByName.Models;

namespace Playground.Application.Features.Country.Command.Create.Interface
{
    public interface IGetByNameCountryRepository
    {
        Task<GetByNameCountryOutput?> CreateToDoItemAsync(GetByNameCountryQuery input, CancellationToken cancellationToken);
    }
}
