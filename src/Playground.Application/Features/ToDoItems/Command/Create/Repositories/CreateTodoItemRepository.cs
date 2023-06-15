﻿using Dapper;
using Playground.Application.Features.ToDoItems.Command.Create.Interface;
using Playground.Application.Features.ToDoItems.Command.Create.Models;
using Playground.Application.Features.ToDoItems.Command.Create.Repositories.Script;
using System.Data;

namespace Playground.Application.Features.ToDoItems.Command.Create.Repositories
{
    public class CreateTodoItemRepository : ICreateTodoItemRepository
    {
        private readonly IDbConnection _connection;

        public CreateTodoItemRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<CreateToDoItemOutput> CreateToDoItemAsync(CreateToDoItemCommand input, CancellationToken cancellationToken)
        {
            return new() { Id = 1 };

            //return await _connection.QueryFirstOrDefaultAsync<CreateToDoItemOutput>(new CommandDefinition(
            //    commandText: CreateTodoItemRepositoryScript.SqlScript,
            //    cancellationToken: cancellationToken,
            //    commandTimeout: 1
            //));
        }
    }
}
