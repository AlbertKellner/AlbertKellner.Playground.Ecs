using Dapper;
using Playground.Application.Features.Country.Command.Create.Interface;
using Playground.Application.Features.Country.Command.Create.Repositories.Script;
using Playground.Application.Features.Country.Query.GetByName.Models;
using System.Data;

namespace Playground.Application.Features.Country.Command.Create.Repositories
{
    public class GetByNameCountryRepository : IGetByNameCountryRepository
    {
        private readonly IDbConnection _connection;

        public GetByNameCountryRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<GetByNameCountryOutput> CreateToDoItemAsync(GetByNameCountryQuery input, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstOrDefaultAsync<GetByNameCountryOutput>(new CommandDefinition(
                commandText: GetByNameCountryRepositoryScript.SqlScript,
                cancellationToken: cancellationToken,
                commandTimeout: 1
            ));
        }
    }
}
