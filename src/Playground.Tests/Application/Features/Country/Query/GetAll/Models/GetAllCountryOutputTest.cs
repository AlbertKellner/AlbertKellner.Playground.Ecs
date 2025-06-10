using Playground.Application.Features.Country.Query.GetAll.Models;

namespace Playground.Tests.Controllers
{
    public class GetAllCountryOutputTest
    {
        [Fact]
        public void IsValid_QuandoNomeNaoVazio_DeveRetornarTrue()
        {
            var output = new GetAllCountryOutput { Name = "Brazil" };

            Assert.True(output.IsValid());
        }
    }
}
