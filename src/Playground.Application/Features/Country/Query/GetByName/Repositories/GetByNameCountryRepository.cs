using Dapper;
using Playground.Application.Features.Country.Query.GetByName.Interface;
using Playground.Application.Features.Country.Query.GetByName.Repositories.Script;
using Playground.Application.Features.Country.Query.GetByName.Models;
using System.Data;

namespace Playground.Application.Features.Country.Query.GetByName.Repositories
{
    public class GetByNameCountryRepository : IGetByNameCountryRepository
    {
        static GetByNameCountryRepository()
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        private readonly IDbConnection _connection;

        public GetByNameCountryRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<GetByNameCountryOutput?> GetByNameCountryAsync(GetByNameCountryQuery input, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstOrDefaultAsync<GetByNameCountryOutput>(new CommandDefinition(
                commandText: GetByNameCountryRepositoryScript.SqlScript,
                cancellationToken: cancellationToken,
                commandTimeout: 1
            ));
        }
    }
}
