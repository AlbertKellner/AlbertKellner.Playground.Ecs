using Playground.Application.Features.Pokemon.GetByName.Models;

namespace Playground.Tests.Controllers
{
    public class GetByNamePokemonOutputTest
    {
        [Fact(DisplayName = "IsValid QuandoNomeNaoVazio DeveRetornarTrue")]
        public void IsValid_QuandoNomeNaoVazio_DeveRetornarTrue()
        {
            var output = new GetByNamePokemonOutput { Name = "pikachu" };

            Assert.True(output.IsValid());
        }
    }
}
