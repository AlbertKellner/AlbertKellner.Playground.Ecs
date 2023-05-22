using Dapper;
using Playground.Application.Features.ToDoItems.Create.Interface;
using Playground.Application.Features.ToDoItems.Create.Models;
using Playground.Application.Features.ToDoItems.Create.Repositories.Script;
using System.Data;

namespace Playground.Application.Features.ToDoItems.Create.Repositories
{
    public class CreateTodoItemRepository : ICreateTodoItemRepository
    {
        private readonly IDbConnection _connection;

        public CreateTodoItemRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<CreateToDoItemOutput> CreateToDoItemAsync(CreateToDoItemInput input, CancellationToken cancellationToken)
        {
            return await _connection.QueryFirstOrDefaultAsync<CreateToDoItemOutput>(new CommandDefinition(
                commandText: CreateTodoItemRepositoryScript.SqlScript,
                cancellationToken: cancellationToken,
                commandTimeout: 1
            ));
        }
    }
}
