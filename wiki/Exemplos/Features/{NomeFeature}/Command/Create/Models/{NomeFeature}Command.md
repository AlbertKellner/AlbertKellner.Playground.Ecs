# {NomeFeature}Command

```csharp
public class {NomeFeature}Command : ValidatableInputBase, IRequest<{NomeFeature}Output>
{
    public string ExampleProperty { get; set; } = string.Empty;

    public override IEnumerable<string> ErrosList()
    {
        var contract = new Contract<Notification>()
            .Requires()
            .IsNotNullOrWhiteSpace(ExampleProperty, nameof(ExampleProperty), "Campo obrigatório");
        return GenerateErrorList(contract);
    }
}
```
