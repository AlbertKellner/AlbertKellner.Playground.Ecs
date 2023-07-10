using Dapper;
using Microsoft.Extensions.Caching.Memory;
using Playground.Application.Features.Country.Command.Create.Interface;
using Playground.Application.Features.Country.Command.Create.Repositories.Script;
using Playground.Application.Features.Country.Query.GetAll.Models;
using Playground.Application.Shared.AsyncLocals;
using System.Data;
using System.Xml.Linq;

namespace Playground.Application.Features.Country.Command.Create.Repositories
{
    public class GetAllCountryRepository : IGetAllCountryRepository
    {
        private readonly IDbConnection _connection;
        private readonly IMemoryCache _memoryCache;

        public GetAllCountryRepository(
            IDbConnection connection,
            IMemoryCache memoryCache)
        {
            _connection = connection;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<GetAllCountryOutput>> GetAllCountryAsync(CancellationToken cancellationToken)
        {
            return await _memoryCache.GetOrCreateAsync($"{CorrelationContext.GetCorrelationId()}", async entry =>
            {
                var result = await _connection.QueryAsync<GetAllCountryOutput>(new CommandDefinition(
                            commandText: GetAllCountryRepositoryScript.SqlScript,
                            cancellationToken: cancellationToken,
                            commandTimeout: 60
                        ));
                
                entry.SetAbsoluteExpiration(result != null ? TimeSpan.FromSeconds(20) : TimeSpan.FromSeconds(3)); //TODO: Extract to configJson

                return result;
            }) ?? new List<GetAllCountryOutput>();
        }
    }
}
