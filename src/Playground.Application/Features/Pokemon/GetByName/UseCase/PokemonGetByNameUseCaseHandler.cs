using MediatR;
using Playground.Application.Features.Pokemon.GetByName.Models;

namespace Playground.Application.Features.Pokemon.GetByName.UseCase
{
    public class PokemonGetByNameUseCaseHandler : IRequestHandler<PokemonGetByNameInput, PokemonGetByNameOutput>
    {
        public async Task<PokemonGetByNameOutput> Handle(PokemonGetByNameInput input, CancellationToken cancellationToken)
        {
            var items = new List<PokemonGetByNameOutput>
            {
                new PokemonGetByNameOutput
                {
                    Name = "Pikachu"
                }
            };

            return items.SingleOrDefault(item => item.Name == input.Name) ?? new PokemonGetByNameOutput();
        }
    }
}
