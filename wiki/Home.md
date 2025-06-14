# Playground ECS

Este projeto demonstra uma API ASP.NET Core construída com .NET 8.0 e estruturada para separar responsabilidades de aplicação, infraestrutura e testes. A solução serve como um repositório de experimentos ("playground") para estudos de padrões de arquitetura, injeção de dependência e boas práticas.

## Estrutura de Pastas

- **src/Playground.ControllerApi**: Projeto principal da API. Contém os controllers, configurações de middleware e inicializações de serviços.
- **src/Playground.Application**: Camada de aplicação onde ficam os casos de uso (Use Cases), serviços, repositórios e integrações externas.
- **src/Playground.Tests**: Projeto de testes unitários utilizando xUnit e Moq.

Cada recurso da aplicação é organizado em _Features_, com subpastas para `Command` (operações de escrita) e `Query` (operações de leitura). As dependências são injetadas via construtor, seguindo o padrão _dependency injection_.

## Principais Bibliotecas e Frameworks

- **ASP.NET Core**: hospeda a API REST.
- **MediatR**: implementa o padrão _CQRS_ e facilita a separação de responsabilidades entre controllers e casos de uso.
- **Autofac**: container de injeção de dependência utilizado na inicialização da aplicação.
- **Dapper**: acesso a dados de forma leve, com scripts separados em arquivos da pasta `Repositories/Script`.
- **Refit**: integração com APIs externas (ex.: `PokemonApi`).
- **Serilog**: logging configurável e enriquecido por _middleware_.
- **Flunt**: validações de entrada nos modelos.

## Injeção de Dependência

A inicialização dos serviços ocorre em `RegisterCustomWebApplicationBuilderInitializer` e `RegisterCustomServicesInitializer`. Esses arquivos registram:

- _Modules_ do Autofac que carregam handlers do MediatR e outras dependências.
- Instâncias de `IMemoryCache`, `IDbConnection` e serviços externos (como `IPokemonApi`).

Controllers e handlers recebem suas dependências via construtor, permitindo a aplicação de testes unitários de forma isolada.

## Regras de Negócio

As regras de negócio estão encapsuladas nos handlers de cada _Feature_. Exemplos de funcionalidades incluídas:

- **Country**: consultas de países utilizando Dapper e cache em memória.
- **Pokemon**: integração com uma API externa para obter dados de Pokémon.
- **ToDoItems**: CRUD completo de tarefas com casos de uso separados para criação, edição, remoção e consulta.

Cada _Feature_ define seus modelos de entrada e saída, validadores e repositórios (quando necessário). Os scripts de banco residem em subpastas `Script`, facilitando a manutenção.

## Execução

Para executar a API localmente:

```bash
dotnet run --project src/Playground.ControllerApi/Playground.csproj
```

Os testes podem ser executados com:

```bash
dotnet test src/Playground.Ecs.sln
```

A solução foi criada em .NET 8.0 e pode servir como base para estudos ou prototipagem de aplicações web.
