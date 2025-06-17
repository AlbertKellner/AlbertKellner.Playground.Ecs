using Playground.Application.Features.Country.Query.GetByName.Models;

namespace Playground.Tests.Controllers
{
    public class GetByNameCountryQueryExtensionsTest
    {
        [Fact(DisplayName = "ToWarning DeveGerarStringCorreta")]
        public void ToWarning_DeveGerarStringCorreta()
        {
            var query = new GetByNameCountryQuery();
            query.SetName("brazil");

            var warning = query.ToWarning();

            Assert.Equal("Name:brazil|FormattedErrosList:()", warning);
        }

        [Fact(DisplayName = "ToInformation DeveGerarStringCorreta")]
        public void ToInformation_DeveGerarStringCorreta()
        {
            var query = new GetByNameCountryQuery();
            query.SetName("brazil");

            var info = query.ToInformation();

            Assert.Equal("Name:brazil", info);
        }
    }
}
