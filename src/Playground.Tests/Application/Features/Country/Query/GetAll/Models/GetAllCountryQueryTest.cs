using Playground.Application.Features.Country.Query.GetAll.Models;

namespace Playground.Tests.Controllers
{
    public class GetAllCountryQueryTest
    {
        [Fact]
        public void ErrosList_DeveEstarVazia()
        {
            var query = new GetAllCountryQuery();

            var erros = query.ErrosList();

            Assert.Empty(erros);
            Assert.False(query.IsInvalid());
            Assert.Equal("()", query.FormattedErrosList());
        }
    }
}
