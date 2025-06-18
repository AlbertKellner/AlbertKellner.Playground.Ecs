# Instruções do AGENT para o repositório Playground.Ecs

Este documento descreve como escrever e organizar testes unitários neste projeto.

## Estrutura de diretórios

- Todos os testes unitários ficam em `src/Playground.Tests`.
- Dentro dessa pasta a organização reproduz os controllers da API. Por exemplo:
  - Controller `PokemonController` -> testes em `src/Playground.Tests/Api/Controller/PokemonController/`.
  - Cada ação do controller possui seu próprio arquivo de teste com o nome `<NomeDaAcao><Controller>Test.cs`.
- O namespace de todas as classes de teste é `Playground.Tests.Controllers`.
- Sempre que criar uma nova pasta de testes, replique exatamente o nome do controller para facilitar a localização dos arquivos.
- Utilize a pasta `Template` como ponto de partida para novas classes.

## Orientações para as classes de teste

- Utilize **xUnit** e coloque `global using Xunit;` em `src/Playground.Tests/Usings.cs`.
- Declare campos privados para todos os mocks (`Mock<IMediator>`, `Mock<ILogger<T>>`, etc.) e para o controller testado.
- Inicialize os mocks e o controller no construtor da classe de teste.
- Siga o padrão **Arrange / Act / Assert** em cada método decorado com `[Fact]`.
  - Os nomes dos métodos seguem o formato `Metodo_QuandoCondicao_DeveRetornarResultado`.
  - Verifique o tipo exato do `IActionResult` retornado (`OkObjectResult`, `BadRequestObjectResult`, etc.) e os valores de `StatusCode` e `Value`.
- Utilize **Moq** para simular dependências.
- Quando necessário, utilize `Verify` nos mocks para garantir que métodos foram executados.
- Empregue `CancellationToken.None` nas chamadas do controlador para simplificar os testes.
- Mantenha cada arquivo com apenas uma classe de teste para facilitar a manutenção.
- Consulte `src/Playground.Tests/Api/Controller/Template/SampleControllerTestTemplate.cs` para um exemplo de estrutura (o arquivo está envolto em `#if false` para não ser compilado).

## Requisitos de workflow

Ao criar ou alterar testes, execute `dotnet build` e `dotnet test` na solução `src/Playground.Ecs.sln` para confirmar que tudo continua compilando e que a suíte de testes está verde.
Qualquer commit só deve ser enviado após estas duas verificações concluírem com sucesso.
A geração de cobertura é automática no CI e o arquivo `coveragereport/Summary.txt`, produzido pelo passo `reportgenerator`, apresenta o resultado.

## Padrões das Controllers e Middlewares

- Os controllers residem em `src/Playground.API/Controllers` e o nome
  do arquivo segue o padrão `<Nome>Controller.cs`.
- Cada controller é decorado com `[ApiController]`, `[ApiVersion]` e `[Route]`.
- As dependências (por exemplo `IMediator` e `ILogger<T>`) são injetadas via
  construtor.
- As ações recebem `CancellationToken` e objetos `Command` ou `Query` das
  features. A validação ocorre através do método `IsInvalid()` e, em caso de
  erro, retorna-se `BadRequest` com `ErrosList()`.
- O retorno usa `Ok`, `CreatedAtRoute`, `NoContent` ou `BadRequest` conforme o
  resultado do `UseCase` executado via MediatR.
- O padrão de log segue `"[Api][NomeController][NomeAcao][Resultado]"` com as
  informações do input através dos métodos `ToInformation()` e `ToWarning()`.
- Versões diferentes da API ficam em subpastas como `Controllers/v2_0`.
- Os middlewares são registrados em `RegisterCustomMiddlewareInitializer` e
  incluem `ExecutionTimeMiddleware`, `BearerTokenMiddleware` e
  `CorrelationIdMiddleware`.

Exemplo resumido de ação em controller:

```csharp
[HttpGet("{name}")]
public async Task<IActionResult> GetByNameAsync(
    [FromRoute] string name,
    [FromQuery] GetByNameCountryQuery input,
    CancellationToken cancellationToken)
{
    input.SetName(name);
    if (input.IsInvalid())
        return BadRequest(input.ErrosList());

    var output = await _mediator.Send(input, cancellationToken);

    if (output.IsValid())
        return Ok(output);

    return NoContent();
}
```

## Estrutura e nomenclatura das Features

- As features ficam em `src/Playground.Application/Features` divididas por
  domínio (`Country`, `Pokemon`, `ToDoItems`).
- Cada domínio contém pastas `Command` ou `Query` com as seguintes convenções:
  - `Models` – classes de entrada (Command/Query) e saída (Output) e possíveis
    classes de extensão;
  - `UseCase` – handlers implementando `IRequestHandler`;
  - `Repositories` – implementações de acesso a dados, com subpasta `Script`
    contendo o SQL utilizado;
  - `Interface` – definição das interfaces de repositório.
- Os nomes de arquivos seguem o padrão `<Operacao><Entidade><Tipo>.cs`, por
  exemplo `GetAllCountryQuery.cs`, `CreateTodoItemRepository.cs` ou
  `PatchTaskNameToDoItemUseCaseHandler.cs`.
- Os handlers recebem repositórios via interface e retornam objetos de saída,
  mantendo baixa acoplamento entre camadas.

Exemplo de modelo e handler em uma feature:

```csharp
// Models/GetByNameCountryQuery.cs
public class GetByNameCountryQuery : ValidatableInputBase,
    IRequest<GetByNameCountryOutput>
{
    public string Name { get; private set; } = string.Empty;

    public void SetName(string name) => Name = name;

    public override IEnumerable<string> ErrosList()
    {
        var contract = new Contract<Notification>()
            .Requires()
            .IsNotNullOrWhiteSpace(Name, nameof(Name), "Nome obrigatório");
        return GenerateErrorList(contract);
    }
}

// UseCase/GetByNameCountryUseCaseHandler.cs
public class GetByNameCountryUseCaseHandler :
    IRequestHandler<GetByNameCountryQuery, GetByNameCountryOutput>
{
    public Task<GetByNameCountryOutput> Handle(
        GetByNameCountryQuery input, CancellationToken cancellationToken)
    {
        var items = new []
        {
            new GetByNameCountryOutput { Name = "Brazil" },
            new GetByNameCountryOutput { Name = "Canada" }
        };
        var result = items.FirstOrDefault(
            item => item.Name.Equals(input.Name, StringComparison.OrdinalIgnoreCase))
            ?? new GetByNameCountryOutput();
        return Task.FromResult(result);
    }
}
```

## Padrões de Logs

- Serilog é configurado em `RegisterCustomWebApplicationBuilderInitializer` com
  `LogEnricher` adicionando propriedades `ExecutionTime`,
  `ExecutionTimeSinceLastLog` e `UserId`.
- `ExecutionTimeMiddleware` inicia a contagem e adiciona o cabeçalho
  `Execution-Time` na resposta.
- `CorrelationIdMiddleware` exige o cabeçalho `CorrelationId`, gerando um novo se estiver ausente, e adiciona o valor ao contexto de logs.
- `BearerTokenMiddleware` lê o token JWT e popula `UserAuthorizationContext`.
- `LogTroubleshooting` permite registrar mensagens adicionais para um `CorrelationId` específico.

Configuração de Serilog e middlewares:

```csharp
public static WebApplicationBuilder RegisterCustomWebApplicationBuilder(
    this WebApplicationBuilder builder)
{
    SerilogConfig(builder, builder.Environment);
    // ... outras inicializações
    return builder;
}

private static void SerilogConfig(WebApplicationBuilder builder,
    IWebHostEnvironment environment)
{
    var loggerConfiguration = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .Enrich.With<LogEnricher>()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .WriteTo.Async(a => a.Console());

    Log.Logger = loggerConfiguration.CreateLogger();
}

public static WebApplication RegisterCustomMiddleware(this WebApplication app)
{
    app.UseMiddleware<ExecutionTimeMiddleware>();
    app.UseMiddleware<BearerTokenMiddleware>();
    app.UseMiddleware<CorrelationIdMiddleware>();
    return app;
}
```

## Padrões de Testes

- Os testes de controllers seguem sempre o fluxo **Arrange / Act / Assert**:
  - *Arrange*: criação dos mocks e definição do comportamento esperado.
  - *Act*: chamada ao método do controller passando `CancellationToken.None`.
  - *Assert*: verificação do tipo de `IActionResult`, `StatusCode`, `Value` e uso
    de `Verify` para conferir chamadas aos mocks.
- Os testes de classes da aplicação (handlers, models e repositórios) utilizam o
  mesmo padrão de três fases e ficam em `src/Playground.Tests/Application`.

Exemplo de teste com Arrange/Act/Assert:

```csharp
[Fact]
public async Task GetByNameAsync_QuandoEntradaValida_DeveRetornarOk()
{
    // Arrange
    _mockMediator
        .Setup(m => m.Send(_validInput, It.IsAny<CancellationToken>()))
        .ReturnsAsync(_validOutput);

    // Act
    var result = await _controller.GetByNameAsync(
        _validInput.Name, _validInput, CancellationToken.None);

    // Assert
    var response = Assert.IsType<OkObjectResult>(result);
    Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
    Assert.Equal(_validOutput, response.Value);
    _mockMediator.Verify(m =>
        m.Send(_validInput, It.IsAny<CancellationToken>()),
        Times.Once);
}
```

## Exemplos completos de Features

A seguir est\u00E1 um exemplo da feature `CreateToDoItem`. Todos os dom\u00EDnios
seguem essa mesma organiza\u00E7\u00E3o de pastas e nomenclatura.

Estrutura da pasta:

```text
src/Playground.Application/Features/ToDoItems/Command/Create
├─ Interface
│  \u2514 ICreateTodoItemRepository.cs
├─ Models
│  \u251C CreateToDoItemCommand.cs
│  \u251C CreateToDoItemCommandExtensions.cs
│  \u2514 CreateTodoItemOutput.cs
├─ Repositories
│  \u251C CreateTodoItemRepository.cs
│  \u2514 Script
│      \u2514 CreateTodoItemRepositoryScript.cs
\u2514 UseCase
   \u2514 CreateTodoItemUseCaseHandler.cs
```

Exemplos de conte\u00FAdo dos arquivos principais:

```csharp
// Interface/ICreateTodoItemRepository.cs
public interface ICreateTodoItemRepository
{
    Task<CreateToDoItemOutput> CreateToDoItemAsync(
        CreateToDoItemCommand input,
        CancellationToken cancellationToken);
}
```

```csharp
// Models/CreateToDoItemCommand.cs
public class CreateToDoItemCommand : ValidatableInputBase,
    IRequest<CreateToDoItemOutput>
{
    public string Task { get; set; } = string.Empty;
    public bool IsCompleted = false;

    public override IEnumerable<string> ErrosList()
    {
        var contract = new Contract<Notification>()
            .Requires()
            .IsNotNullOrWhiteSpace(Task, nameof(Task),
                $"{nameof(Task)} n\u00E3o pode ser vazio ou somente espa\u00E7os em branco");
        return GenerateErrorList(contract);
    }
}
```

```csharp
// Models/CreateToDoItemCommandExtensions.cs
public static class CreateToDoItemCommandExtensions
{
    public static string ToWarning(this CreateToDoItemCommand input) =>
        $"{nameof(input.Task)}:{input.Task}|{nameof(input.IsCompleted)}:{input.IsCompleted}";

    public static string ToInformation(this CreateToDoItemCommand input) =>
        $"{nameof(input.Task)}:{input.Task}|{nameof(input.IsCompleted)}:{input.IsCompleted}";
}
```

```csharp
// Models/CreateTodoItemOutput.cs
public class CreateToDoItemOutput
{
    public long Id { get; set; }
    public string Task { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }

    public bool IsCreated() => Id > 0;
}
```

```csharp
// UseCase/CreateTodoItemUseCaseHandler.cs
public class CreateTodoItemUseCaseHandler :
    IRequestHandler<CreateToDoItemCommand, CreateToDoItemOutput>
{
    private readonly ICreateTodoItemRepository _repository;

    public CreateTodoItemUseCaseHandler(ICreateTodoItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateToDoItemOutput> Handle(
        CreateToDoItemCommand input, CancellationToken cancellationToken)
    {
        return await _repository.CreateToDoItemAsync(input, cancellationToken);
    }
}
```

```csharp
// Repositories/CreateTodoItemRepository.cs
public class CreateTodoItemRepository : ICreateTodoItemRepository
{
    private readonly IDbConnection _connection;

    public CreateTodoItemRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<CreateToDoItemOutput> CreateToDoItemAsync(
        CreateToDoItemCommand input, CancellationToken cancellationToken)
    {
        return await _connection.QueryFirstOrDefaultAsync<CreateToDoItemOutput>(
            new CommandDefinition(
                CreateTodoItemRepositoryScript.SqlScript,
                cancellationToken: cancellationToken));
    }
}
```

```csharp
// Repositories/Script/CreateTodoItemRepositoryScript.cs
internal static class CreateTodoItemRepositoryScript
{
    internal const string SqlScript =
        @"SELECT * FROM TABLE";
}
```

Para consultas (Queries) o padr\u00E3o \u00E9 o mesmo. A feature `GetByIdToDoItem` possui:

```text
src/Playground.Application/Features/ToDoItems/Query/GetById
├─ Models
│  \u251C GetByIdToDoItemQuery.cs
│  \u251C GetByIdToDoItemQueryExtensions.cs
│  \u2514 GetByIdToDoItemOutput.cs
\u2514 UseCase
   \u2514 GetByIdToDoItemUseCaseHandler.cs
```

## Exemplos de Controllers

Os controllers residem em `src/Playground.API/Controllers`. Cada
arquivo injeta `IMediator` e `ILogger<T>` no construtor e as a\u00E7\u00F5es validam a
entrada, registram logs e retornam `IActionResult`.

Exemplo resumido de `ToDoItemController`:

```csharp
[ApiController]
[ApiVersion("1.0")]
[Route("todo")]
public class ToDoItemController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ToDoItemController> _logger;

    public ToDoItemController(IMediator mediator, ILogger<ToDoItemController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{id:long}", Name = "GetById")]
    public async Task<IActionResult> GetByIdAsync(
        long id,
        [FromQuery] GetByIdToDoItemQuery input,
        CancellationToken cancellationToken)
    {
        input.SetId(id);
        if (input.IsInvalid())
            return BadRequest(input.ErrosList());

        var output = await _mediator.Send(input, cancellationToken);

        if (output.IsValid())
            return Ok(output);

        return NoContent();
    }
}
```
