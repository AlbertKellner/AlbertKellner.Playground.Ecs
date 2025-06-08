using Playground.Application.Features.Country.Query.GetByName.Models;

namespace Playground.Application.Features.Country.Query.GetByName.Interface
{
    public interface IGetByNameCountryRepository
    {
        Task<GetByNameCountryOutput?> GetByNameCountryAsync(GetByNameCountryQuery input, CancellationToken cancellationToken);
    }
}
