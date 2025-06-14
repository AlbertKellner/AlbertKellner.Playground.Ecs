using Playground.Application.Features.Country.Query.GetByName.Models;

namespace Playground.Tests.Controllers
{
    public class GetByNameCountryQueryTest
    {
        [Fact]
        public void SetName_DeveAlterarNome()
        {
            var query = new GetByNameCountryQuery();
            query.SetName("Canada");

            Assert.Equal("Canada", query.Name);
        }

        [Fact]
        public void ErrosList_QuandoNomeVazio_DeveRetornarErro()
        {
            var query = new GetByNameCountryQuery();

            var erros = query.ErrosList().ToList();

            Assert.NotEmpty(erros);
            Assert.True(query.IsInvalid());
            Assert.Equal($"({string.Join("|", erros)})", query.FormattedErrosList());
        }
    }
}
