using MediatR;
using Playground.Application.Features.Country.Query.GetByName.Models;

namespace Playground.Application.Features.Country.Query.GetByName.UseCase
{
    public class GetByNameCountryUseCaseHandler : IRequestHandler<GetByNameCountryQuery, GetByNameCountryOutput>
    {
        public async Task<GetByNameCountryOutput> Handle(GetByNameCountryQuery input, CancellationToken cancellationToken)
        {
            var items = new List<GetByNameCountryOutput>
            {
                new GetByNameCountryOutput { Name = "Brazil" },
                new GetByNameCountryOutput { Name = "Canada" }
            };

            return items.SingleOrDefault(item => item.Name.Equals(input.Name, StringComparison.OrdinalIgnoreCase))
                ?? new GetByNameCountryOutput();
        }
    }
}
