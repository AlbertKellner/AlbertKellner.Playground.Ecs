# {NomeFeature}Handler

```csharp
public class {NomeFeature}Handler : IRequestHandler<{NomeFeature}Command, {NomeFeature}Output>
{
    private readonly I{NomeFeature}Repository _repository;
    public {NomeFeature}Handler(I{NomeFeature}Repository repository) => _repository = repository;

    public async Task<{NomeFeature}Output> Handle({NomeFeature}Command input, CancellationToken cancellationToken)
    {
        return await _repository.ExecuteAsync(input, cancellationToken);
    }
}
```
