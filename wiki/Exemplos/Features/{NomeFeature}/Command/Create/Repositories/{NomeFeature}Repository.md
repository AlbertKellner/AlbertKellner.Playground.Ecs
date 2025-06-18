# {NomeFeature}Repository

```csharp
public class {NomeFeature}Repository : I{NomeFeature}Repository
{
    private readonly IDbConnection _connection;
    public {NomeFeature}Repository(IDbConnection connection) => _connection = connection;

    public async Task<{NomeFeature}Output> ExecuteAsync({NomeFeature}Command input, CancellationToken cancellationToken)
    {
        return await _connection.QueryFirstOrDefaultAsync<{NomeFeature}Output>(
            new CommandDefinition(
                {NomeFeature}RepositoryScript.SqlScript,
                cancellationToken: cancellationToken));
    }
}
```
