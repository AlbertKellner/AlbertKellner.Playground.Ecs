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
                new GetByNameCountryOutput
                {
                    Id = 99,
                    Task = "GetById - ToDoItem - UseCaseHandler",
                    IsCompleted = true
                }
            };

            return items.SingleOrDefault(item => item.Id == input.Id) ?? new GetByNameCountryOutput();
        }
    }
}
