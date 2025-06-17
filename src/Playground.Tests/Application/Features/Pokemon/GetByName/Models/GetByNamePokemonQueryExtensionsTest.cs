using Playground.Application.Features.Pokemon.GetByName.Models;

namespace Playground.Tests.Controllers
{
    public class GetByNamePokemonQueryExtensionsTest
    {
        [Fact(DisplayName = "ToWarning DeveGerarStringCorreta")]
        public void ToWarning_DeveGerarStringCorreta()
        {
            var query = new GetByNamePokemonQuery();
            query.SetName("pikachu");

            var warning = query.ToWarning();

            Assert.Equal("Name:pikachu|FormattedErrosList:()", warning);
        }

        [Fact(DisplayName = "ToError DeveGerarStringCorreta")]
        public void ToError_DeveGerarStringCorreta()
        {
            var query = new GetByNamePokemonQuery();
            query.SetName("pikachu");

            var error = query.ToError();

            Assert.Equal("Name:pikachu", error);
        }

        [Fact(DisplayName = "ToInformation DeveGerarStringCorreta")]
        public void ToInformation_DeveGerarStringCorreta()
        {
            var query = new GetByNamePokemonQuery();
            query.SetName("pikachu");

            var info = query.ToInformation();

            Assert.Equal("Name:pikachu", info);
        }
    }
}
