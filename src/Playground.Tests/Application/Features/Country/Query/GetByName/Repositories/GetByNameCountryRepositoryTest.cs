using Moq;
using Moq.Dapper;
using Dapper;
using Playground.Application.Features.Country.Query.GetByName.Repositories;
using Playground.Application.Features.Country.Query.GetByName.Models;
using Playground.Application.Features.Country.Query.GetByName.Repositories.Script;
using System.Data;

namespace Playground.Tests.Controllers
{
    public class GetByNameCountryRepositoryTest
    {
        private readonly Mock<IDbConnection> _mockConnection;
        private readonly GetByNameCountryRepository _repository;

        public GetByNameCountryRepositoryTest()
        {
            _mockConnection = new Mock<IDbConnection>();
            _repository = new GetByNameCountryRepository(_mockConnection.Object);
        }

        [Fact(DisplayName = "GetByNameCountryAsync DeveExecutarQuery")]
        public async Task GetByNameCountryAsync_DeveExecutarQuery()
        {
            var query = new GetByNameCountryQuery();
            query.SetName("Brazil");
            var expected = new GetByNameCountryOutput { Name = "Brazil" };

            _mockConnection.SetupDapperAsync(c => c.QueryFirstOrDefaultAsync<GetByNameCountryOutput>(It.IsAny<CommandDefinition>()))
                .ReturnsAsync(expected);

            var result = await _repository.GetByNameCountryAsync(query, CancellationToken.None);

            Assert.Equal(expected.Name, result?.Name);
        }
    }
}
