# Exemplos de Features

A estrutura abaixo ilustra como organizar uma nova feature. Cada arquivo de exemplo está disponível nesta pasta replicando a mesma hierarquia usada no código fonte.

```
src/Playground.Application/Features/{NomeFeature}/Command/Create
├─ Interface
│  └ I{NomeFeature}Repository.cs
├─ Models
│  ├ {NomeFeature}Command.cs
│  ├ {NomeFeature}CommandExtensions.cs
│  └ {NomeFeature}Output.cs
├─ Repositories
│  ├ {NomeFeature}Repository.cs
│  └ Script
│      └ {NomeFeature}RepositoryScript.cs
└ UseCase
   └ {NomeFeature}Handler.cs
```

Consulte cada arquivo desta pasta para usar como ponto de partida ao desenvolver novas features.
