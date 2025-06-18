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
