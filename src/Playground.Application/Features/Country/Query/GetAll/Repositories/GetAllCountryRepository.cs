using Dapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Playground.Application.Features.Country.Query.GetAll.Interface;
using Playground.Application.Features.Country.Query.GetAll.Repositories.Script;
using Playground.Application.Features.Country.Query.GetAll.Models;
using Playground.Application.Shared.AsyncLocals;
using System.Data;

namespace Playground.Application.Features.Country.Query.GetAll.Repositories
{
    public class GetAllCountryRepository : IGetAllCountryRepository
    {
        static GetAllCountryRepository()
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        private readonly IDbConnection _connection;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<GetAllCountryRepository> _logger;

        public GetAllCountryRepository(
            IDbConnection connection,
            IMemoryCache memoryCache,
            ILogger<GetAllCountryRepository> logger)
        {
            _connection = connection;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task<IEnumerable<GetAllCountryOutput>> GetAllCountryAsync(CancellationToken cancellationToken)
        {
            var cacheKey = $"{CorrelationContext.GetCorrelationId()}";

            _logger.LogInformation("[GetAllCountryRepository][GetAllCountryAsync] Iniciando consulta no cache. CacheKey:{@cacheKey}", cacheKey);

            var cachedResult = TryGetCachedResult(cacheKey);

            if (cachedResult == null || cachedResult == Enumerable.Empty<GetAllCountryOutput>())
            {
                _logger.LogInformation("[GetAllCountryRepository][GetAllCountryAsync] Iniciando criação de cache");

                cachedResult = await CreateResultInCacheAsync(cacheKey, cancellationToken);
            }

            return cachedResult;
        }

        private IEnumerable<GetAllCountryOutput> TryGetCachedResult(string cacheKey)
        {
            if (_memoryCache.TryGetValue(cacheKey, out var tryGetCachedResult))
            {
                _logger.LogInformation("[GetAllCountryRepository][TryGetCachedResult] Retornando cache preenchido");

                return (IEnumerable<GetAllCountryOutput>?)tryGetCachedResult ?? Enumerable.Empty<GetAllCountryOutput>();
            }
            else
            {
                _logger.LogInformation("[GetAllCountryRepository][TryGetCachedResult] Retornando cache vazio");

                return Enumerable.Empty<GetAllCountryOutput>();
            }
        }

        private async Task<IEnumerable<GetAllCountryOutput>> CreateResultInCacheAsync(string cacheKey, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[GetAllCountryRepository][TryGetCachedResult] Iniciando verificação de cache");

            var cacheResult = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
                {
                    _logger.LogInformation("[GetAllCountryRepository][TryGetCachedResult] Iniciando consulta no banco de dados");

                    var databaseResult = await _connection.QueryAsync<GetAllCountryOutput>(new CommandDefinition(
                                            commandText: GetAllCountryRepositoryScript.SqlScript,
                                            cancellationToken: cancellationToken,
                                            commandTimeout: 60
                    ));

                    entry.SetAbsoluteExpiration(GetExpirationTime(databaseResult, cacheKey));

                    _logger.LogInformation("[GetAllCountryRepository][TryGetCachedResult] Retornando consulta no banco de dados");

                    return databaseResult ?? Enumerable.Empty<GetAllCountryOutput>();
                });

            _logger.LogInformation("[GetAllCountryRepository][TryGetCachedResult] Retornando dados do cache");

            return cacheResult ?? Enumerable.Empty<GetAllCountryOutput>();
        }

        private TimeSpan GetExpirationTime(IEnumerable<GetAllCountryOutput> databaseResult, string cacheKey)
        {
            const int databaseResultWithResultExpiration = 20;
            const int databaseResultWithoutResultExpiration = 3;

            if (databaseResult.Any())
            {
                _logger.LogInformation("[GetAllCountryRepository][SetExpirationTime] Criando cache do banco de dados preenchido. cacheKey:{@cacheKey}, Expiração:{@Expiration}", cacheKey, databaseResultWithResultExpiration);

                return TimeSpan.FromSeconds(databaseResultWithResultExpiration);
            }
            else
            {
                _logger.LogInformation("[GetAllCountryRepository][SetExpirationTime] Criando cache do banco de dados vazio. cacheKey:{@cacheKey}, Expiração:{@Expiration}", cacheKey, databaseResultWithoutResultExpiration);

                return TimeSpan.FromSeconds(databaseResultWithoutResultExpiration);
            }
        }
    }
}
