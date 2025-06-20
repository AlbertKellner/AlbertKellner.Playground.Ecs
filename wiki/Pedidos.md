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
Alterando somente a classe GetByIdToDoItemUseCaseHandler, melhore sua performance. Antes de fazer commit, sempre rode o teste de performance e exiba os resultados
```

## Pedido 22
```
Alterando o que for necessario, melhore sua performance da classe GetByIdToDoItemUseCaseHandler. Antes de fazer commit, sempre rode o teste de performance e exiba os resultados
```

## Pedido 23
```
Para tentar maximizar o desempenho, utilize memory cache
```
