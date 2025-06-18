# Repositories

Repositories provide data access implementations.

Example of `CreateTodoItemRepository`:
```csharp
public class CreateTodoItemRepository : ICreateTodoItemRepository
{
    private readonly IDbConnection _connection;
    public CreateTodoItemRepository(IDbConnection connection) => _connection = connection;
    public async Task<CreateToDoItemOutput> CreateToDoItemAsync(CreateToDoItemCommand input, CancellationToken ct)
    {
        return await _connection.QueryFirstOrDefaultAsync<CreateToDoItemOutput>(new CommandDefinition(
            CreateTodoItemRepositoryScript.SqlScript,
            cancellationToken: ct,
            commandTimeout: 1)) ?? new CreateToDoItemOutput();
    }
}
```
