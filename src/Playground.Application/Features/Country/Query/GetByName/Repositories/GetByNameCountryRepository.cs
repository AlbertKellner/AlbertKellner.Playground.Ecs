using Dapper;
using Playground.Application.Features.Country.Query.GetByName.Interface;
using Playground.Application.Features.Country.Query.GetByName.Repositories.Script;
using Playground.Application.Features.Country.Query.GetByName.Models;
using Playground.Application.Infrastructure.Extensions;
using System.Data;

namespace Playground.Application.Features.Country.Query.GetByName.Repositories
{
    public class GetByNameCountryRepository : IGetByNameCountryRepository
    {
        private readonly IDbConnection _connection;

        public GetByNameCountryRepository(IDbConnection connection)
        {
            _connection = connection;

            DapperMappingExtensions.RegisterMappings();
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
