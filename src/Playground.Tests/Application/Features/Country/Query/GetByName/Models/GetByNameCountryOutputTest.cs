using Playground.Application.Features.Country.Query.GetByName.Models;

namespace Playground.Tests.Controllers
{
    public class GetByNameCountryOutputTest
    {
        [Fact]
        public void IsValid_QuandoNomeNaoVazio_DeveRetornarTrue()
        {
            var output = new GetByNameCountryOutput { Name = "Brazil" };

            Assert.True(output.IsValid());
        }
    }
}
