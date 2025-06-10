# Instruções do AGENT para o repositório Playground.Ecs

Este documento descreve como escrever e organizar testes unitários neste projeto.

## Estrutura de diretórios

- Todos os testes unitários ficam em `src/Playground.Tests`.
- Dentro dessa pasta a organização reproduz os controllers da API. Por exemplo:
  - Controller `CountryController` -> testes em `src/Playground.Tests/Api/Controller/CountryController/`.
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

Sempre que modificar ou criar testes, execute `dotnet build` e `dotnet test` no `src/Playground.Ecs.sln` para garantir que o projeto compila e que os testes passam.
No CI do repositório a cobertura de código é calculada automaticamente e o resumo pode ser conferido no arquivo `coveragereport/Summary.txt` gerado pelo passo `reportgenerator`.
