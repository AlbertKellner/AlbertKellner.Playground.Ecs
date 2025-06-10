using Playground.Application.Features.Pokemon.GetByName.Models;

namespace Playground.Tests.Controllers
{
    public class GetByNamePokemonOutputTest
    {
        [Fact]
        public void IsValid_QuandoNomeNaoVazio_DeveRetornarTrue()
        {
            var output = new GetByNamePokemonOutput { Name = "pikachu" };

            Assert.True(output.IsValid());
        }
    }
}
