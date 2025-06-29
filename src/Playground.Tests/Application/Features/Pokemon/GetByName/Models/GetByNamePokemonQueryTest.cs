using Playground.Application.Features.Pokemon.GetByName.Models;

namespace Playground.Tests.Controllers
{
    public class GetByNamePokemonQueryTest
    {
        [Fact(DisplayName = "SetName DeveAlterarNome")]
        public void SetName_DeveAlterarNome()
        {
            var query = new GetByNamePokemonQuery();
            query.SetName("pikachu");

            Assert.Equal("pikachu", query.Name);
        }

        [Fact(DisplayName = "ToWarning DeveGerarStringCorreta")]
        public void ToWarning_DeveGerarStringCorreta()
        {
            var query = new GetByNamePokemonQuery();
            query.SetName("abc");

            var warning = query.ToWarning();

            Assert.Contains("Name:abc", warning);
        }
    }
}
