using Dapper;
using Playground.Application.Features.Country.Query.GetByName.Interface;
using Playground.Application.Features.Country.Query.GetByName.Repositories.Script;
using Playground.Application.Features.Country.Query.GetByName.Models;
using System.Data;

namespace Playground.Application.Features.Country.Query.GetByName.Repositories
{
    public class GetByNameCountryRepository : IGetByNameCountryRepository
    {
        private readonly IDbConnection _connection;

        public GetByNameCountryRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<GetByNameCountryOutput> GetByNameAsync(GetByNameCountryQuery input, CancellationToken cancellationToken)
        {
            var result = await _connection.QueryFirstOrDefaultAsync<GetByNameCountryOutput>(new CommandDefinition(
                commandText: GetByNameCountryRepositoryScript.SqlScript,
                cancellationToken: cancellationToken,
                commandTimeout: 1
            ));

            return result ?? new GetByNameCountryOutput();
        }
    }
}
