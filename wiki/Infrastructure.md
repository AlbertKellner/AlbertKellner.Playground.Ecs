# Infrastructure

This section describes how to build and test the project.

## Build
```bash
dotnet restore src/Playground.Ecs.sln --no-cache --ignore-failed-sources
dotnet build src/Playground.Ecs.sln -c Release --no-restore
```

## Tests
```bash
dotnet test src/Playground.Ecs.sln -c Release --no-build
```

## GitHub Actions
A workflow publishes the contents of the `wiki` folder to the repository Wiki whenever a pull request is merged into `main`. The file is `.github/workflows/publish-wiki.yml` and it uses `Andrew-Chen-Wang/github-wiki-action`.
Another workflow '.github/workflows/benchmark.yml' executes the BenchmarkDotNet project whenever a pull request is opened or synchronized.
