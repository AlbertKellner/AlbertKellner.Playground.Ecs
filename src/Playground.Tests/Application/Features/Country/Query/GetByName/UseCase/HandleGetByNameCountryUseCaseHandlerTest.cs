using Playground.Application.Features.Country.Query.GetByName.UseCase;
using Playground.Application.Features.Country.Query.GetByName.Models;

namespace Playground.Tests.Controllers
{
    public class HandleGetByNameCountryUseCaseHandlerTest
    {
        private readonly GetByNameCountryUseCaseHandler _handler;

        public HandleGetByNameCountryUseCaseHandlerTest()
        {
            _handler = new GetByNameCountryUseCaseHandler();
        }

        [Fact]
        public async Task Handle_QuandoNomeExistente_DeveRetornarPais()
        {
            var query = new GetByNameCountryQuery();
            query.SetName("brazil");

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Equal("Brazil", result.Name);
        }

        [Fact]
        public async Task Handle_QuandoNomeNaoExistente_DeveRetornarVazio()
        {
            var query = new GetByNameCountryQuery();
            query.SetName("unknown");

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Equal(string.Empty, result.Name);
        }
    }
}
