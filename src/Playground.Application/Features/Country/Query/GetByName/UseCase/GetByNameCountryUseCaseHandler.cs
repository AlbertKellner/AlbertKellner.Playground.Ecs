using MediatR;
using Playground.Application.Features.Country.Query.GetByName.Models;

namespace Playground.Application.Features.Country.Query.GetByName.UseCase
{
    public class GetByNameCountryUseCaseHandler : IRequestHandler<GetByNameCountryQuery, GetByNameCountryOutput>
    {
        public Task<GetByNameCountryOutput> Handle(GetByNameCountryQuery input, CancellationToken cancellationToken)
        {
            var items = new List<GetByNameCountryOutput>
            {
                new GetByNameCountryOutput { Name = "Brazil" },
                new GetByNameCountryOutput { Name = "Canada" }
            };

            var result = items.SingleOrDefault(item => item.Name.Equals(input.Name, StringComparison.OrdinalIgnoreCase))
                ?? new GetByNameCountryOutput();
            return Task.FromResult(result);
        }
    }
}
