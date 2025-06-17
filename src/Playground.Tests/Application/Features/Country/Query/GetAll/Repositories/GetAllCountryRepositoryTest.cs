using Moq;
using Moq.Dapper;
using Dapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Playground.Application.Features.Country.Query.GetAll.Repositories;
using Playground.Application.Features.Country.Query.GetAll.Models;
using System.Data;

namespace Playground.Tests.Controllers
{
    public class GetAllCountryRepositoryTest
    {
        private readonly Mock<IDbConnection> _mockConnection;
        private readonly IMemoryCache _memoryCache;
        private readonly Mock<ILogger<GetAllCountryRepository>> _mockLogger;
        private readonly GetAllCountryRepository _repository;

        public GetAllCountryRepositoryTest()
        {
            _mockConnection = new Mock<IDbConnection>();
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _mockLogger = new Mock<ILogger<GetAllCountryRepository>>();
            _repository = new GetAllCountryRepository(_mockConnection.Object, _memoryCache, _mockLogger.Object);
        }

        [Fact(DisplayName = "GetAllCountryAsync DeveUtilizarCache")]
        public async Task GetAllCountryAsync_DeveUtilizarCache()
        {
            var expected = new List<GetAllCountryOutput> { new GetAllCountryOutput { Name = "Brazil" } };
            _mockConnection.SetupDapperAsync(c => c.QueryAsync<GetAllCountryOutput>(It.IsAny<CommandDefinition>()))
                .ReturnsAsync(expected);

            var first = await _repository.GetAllCountryAsync(CancellationToken.None);
            var second = await _repository.GetAllCountryAsync(CancellationToken.None);

            Assert.Single(first);
            Assert.Equal("Brazil", first.First().Name);
            Assert.Equal(first, second);
        }
    }
}
