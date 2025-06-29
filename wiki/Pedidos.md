# Pedidos do Usuário

Abaixo estão registrados, em ordem cronológica, todos os pedidos feitos pelo usuário. Sempre que houver uma nova solicitação, o texto deverá ser adicionado ao final deste arquivo.

## Pedido 1
```
Documentos também o arquivo Agents, colocando nesse arquivo de documentação o texto integral sem alterações.
Adicione no arquivo de Agents, que cada alteração nele deve ser documentada no respectivo arquivo da Wiki, com o texto integral desse arquivo.
Na documentação, adiciona uma sessão chamada exemplos, e adicione todas as actions em forma de template, usando como exemplo todos os verbos disponíveis eu usando um Todo como exemplo. Deixe claro que esses arquivos são dizendo de referência para criação de novos arquivos
```

## Pedido 2
```
Não, a documentação deve ser na Wiki, separando em pastas e arquivos. Cada arquivo deve ser uma sessão das quais eu citei
```

## Pedido 3
```
Cria documentação do projeto, documentando toda a parte de infraestrutura, como executar o build e os testes do projeto, as actions das controllers (adicione o curl de cada endpoint), cada feature, cada midleware com o código respectivo e os repositórios
```

## Pedido 4
```
Crie também um arquivo de documentação onde todos os meus pedidos devem ser registrados lá, com o texto integral. Adicione no agents que cada pedido que eu fizer aqui deve ser documentado nesse mesmo arquivo de forma incremental. Adicione no também na documentação os exemplos de como criar novas Features, documentando a estrutura de pastas o padrão do nome dos arquivos usando também Todo para o exemplo e exemplos de código para cada arquivo dentro da Feature. Na documentação das actions de exemplo, também coloque os exemplos de cada verbo. nos exemplos das actions e features, use chaves e o nome da feature para exemplificar nome dos métodos e arquivos, exemplo: para actions de GetById use “{NomeFeature}GetById”, para o handler use “{NomeFeature}Handler”, para a query dentro da feature use “{NomeFeature}Query”.
```

## Pedido 5
```
Coloque a documentação das controllers, features e modlewares em pastas, e essas pastas dentro das pastas respectivas com os nomes dos seus projetos
```

## Pedido 6
```
Separe a documentação das funcionalidades da documentação de exemplos, os exemplos devem estar dentro de uma pasta chamada exemplos. Ao invés de colocar no exemplo da palavra Todo, use o padrão que eu citei usando “{NomeFeature}”. Cada pedido no arquivo pedidos, deve estar decorado com ````, para facilitar a leitura e a cópia desse conteúdo. Separe a documentação do código atual do repositório, da documentação de exemplos. Crie um arquivo de página inicial dos exemplos explicando que esses exemplos devem ser seguidos para criação de novos arquivos
```

## Pedido 7
```
Na documentação das controles e suas actions, existentes, coloque a action por completo, colocando toda a sua lógica para exemplificar como criar novas actions. No arquivo de Agents, adicione o comando de que toda a nova funcionalidade deve ser consultada nos arquivos de exemplo e esses exemplos devem ser seguidos para a criação dessas novas funcionalidades. Tanto Actions, nas Controllers, quanto Features, Repositórios, middlewares, etc. Na documentação de Features e Controllers de Exemplos, cada arquivo de exemplo deve ser um documento de documentação separado, e a organização desses arquivos deve seguir a mesma estrutura de pastas das suas respectivas controllers ou features.
```

## Pedido 8
```
Os arquivos de documentação não estão aparecendo na Wiki. Crie uma Action para publicá-los sempre que um pull request for mergeado para main.
```

## Pedido 9
```
Utilize essa action para publicar a documentação 
“name: Publicar Wiki
on:
  pull_request:
    branches: ["main"]
    paths:
      - 'wiki/**'
      - '.github/workflows/publish-wiki.yml'
concurrency:
  group: publish-wiki
  cancel-in-progress: true
permissions:
  contents: write
jobs:
  publicar-wiki:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      - uses: Andrew-Chen-Wang/github-wiki-action@v4
        with:
          path: wiki”
```


## Pedido 10
```
Adicione uma action, para adicionar cada pull request criado ao projeto desse repositório. 
O link para o projeto é: https://github.com/users/AlbertKellner/projects/2
```

## Pedido 11
```
Ao invés de importar essa action, é possível colocar o código inteiro aqui? Com o objetivo de desacoplar essa action?
```


## Pedido 12
```
Pesquise na internet se é dessa maneira que adiciona um pull request a um projeto, através de uma action
```
## Pedido 13
```
usando o framework Benchmark.net, faça um teste de performance da classe GetByIdToDoItemUseCaseHandler, crie um projeto a parte para armazenar esse tipo de teste
```

## Pedido 14
```
Crie uma action para rodar esse teste quando abrir ou atualizar um pull request
```
## Pedido 15
```
Duplique a feature de GetByIdToDoItem, adicionando a palavra “Old” após todas as palavras “ToDoItem”
```

## Pedido 16
```
Crie um teste de performance para GetByIdToDoItemOldUseCaseHandler idêntico ao GetByIdToDoItemUseCaseHandler
```
## Pedido 17
```
Ps 2 testes de performance devem estar no mesmo arquivo de execução para criar um comparativo
```

## Pedido 18
```
Os testes estao quebrando, corrija
```

## Pedido 19
```
O pipeline deu o seguinte erro:
Run dotnet run --project src/Playground.Benchmarks/Playground.Benchmarks.csproj -c Release
Available Benchmark:
  #0 GetByIdToDoItemComparisonBenchmark

You should select the target benchmark(s). Please, print a number of a benchmark (e.g. `0`) or a contained benchmark caption (e.g. `GetByIdToDoItemComparisonBenchmark`).
If you want to select few, please separate them with space ` ` (e.g. `1 2 3`).
You can also provide the class name in console arguments by using --filter. (e.g. `--filter '*GetByIdToDoItemComparisonBenchmark*'`).
Enter the asterisk `*` to select all.
To print all available benchmarks use `--list flat` or `--list tree`.
To learn more about filtering use `--help`.
```

## Pedido 20
```
Alterando somente a classe GetByIdToDoItemUseCaseHandler, melhore sua performance. Antes de fazer commit, sempre rode o teste de performance e exiba os resultados
```
## Pedido 21
```
Crie u a nova feature, para integrar com o chatGPT, vou enviar minhas chaves de acesso nem outro prompt. A feature deve chamar OpenIaIntegrationFeature. Crie a Controller mo as respectivas actions. A conexão com o ChatGPT deve estar na camada equivalente de consumo de APis. O Objetivo é enviar um prompt e receber a resposta em texto. Quero ter a opção de controlar temperatura, modelo da LLM, e etc.
```
## Pedido 22
```
Para a chave ’ExternalApiOptions’, crie um conjunto pata o PokemonApi e outro para o OpenIaApi.
Ajuste os padrões da feature e controller da OpenIA, usando a documentação
```

## Pedido 23
```
Crie um novo projeto para testes de integração, chamado IntegrationTests.
Crie um teste de integração para testar a controller ToDoItemController, usando o endpoint ToDoItemController.

Esse projeto de testes deve subir a aplicação e chamar o endpoint para testar sua execução. 
Quais frameworks poderíamos utilizar? Já vi o wiremock para essa abordagem.

Alguma duvida?

Sempre responsável em português 
```

## Pedido 24
```
Como você poderia criar esse teste, mochando um servidor e subindo a aplicação?
```

## Pedido 25
```
Adicione aos agents, que sempre antes de um comitê, execute tambem os testes de integração 
Crie tambem uma action para executa-los, quando criar ou  atualizar um pull request
```

## Pedido 26
```
Adicione no teste, a certificação do status code correto, e abra o json de retorno e verifique se o item foi respondido corretamente
```

## Pedido 27
```
Adicione testes para todos os status code
```

## Pedido 28
```
Envie correlationId em todas as requisições
```

## Pedido 29
```
A Action Vincular Pull Requests ao Projeto, esta dando o seguinte erro:

Run ./scripts/add-pr-to-project.sh https://github.com/users/AlbertKellner/projects/2
  ./scripts/add-pr-to-project.sh https://github.com/users/AlbertKellner/projects/2
  shell: /usr/bin/bash -e {0}
  env:
    GITHUB_TOKEN: ***
./scripts/add-pr-to-project.sh: line 23: owner: unbound variable
Error: Process completed with exit code 1.

O que pode ser? Faça uma análise completa
Responsável em português

Pode corrigir
```

## Pedido 30
```
Faça testes de integração do endpoint pokemon
```
## Pedido 31
```
Agora adicione um teste que não faz Mock da apiPokemon, e sim, consome ela de verdade. Diferencie os testes criando uma classe para cada, um mocado, e um sem mocks
```

## Pedido 32
```
Tire o Skip delas
```

## Pedido 33
```
Os testes estao quebrando
```

## Pedido 34
```
No teste integrado que não é no mockado, classe ToDoItemControllerIntegrationTest, verifique se não aparece nenhum erro no log
```

## Pedido 35
```
No teste integrado da classe ToDoItemControllerIntegrationTest, grave a saída dos logs em um arquivo com o nome do método de teste seguido por a palavra ExecutionLogs
```

## Pedido 36
```
No teste integrado classe ToDoItemControllerIntegrationTest, verifique se a requisição demorou menos de 1 segundo
```


## Pedido 37
```
Crie um jeito mais elegante de calcular o tempo de execução do teste
```

\n## Pedido 38



## Pedido 38
```
Com base na PokemonController, quero que você extraia cada action para sua respectiva feature. Coloque as actions na pasta chamada Endpoint, em suas respectivas features. Os endpoints devem continuar funcionando. Garanta que ainda funciona, pelos testes de integração. Não altere os testes de integração.
```

## Pedido 39
```
Você deletou os testes das controllers. Não delete, adapte para testar os novos métodos de endpoint.
```

## Pedido 40
```
As interfaces dos endpoints devem estar na passar de interface, em suas respectivas features 
As classes de teste dos endpoints devem ter os mesmos nomes que tinham as classes de teste da controller. 
```

## Pedido 41
```
O arquivo de teste deve estar com o mesmo nome, mas mesma pasta, pra que eu possa ver a diferenças no código da migração de endpoint
```
