# AGENT instructions for Playground.Ecs repository

This file documents the conventions for writing unit tests in this project.

## Test directory layout

- All unit tests live under `src/Playground.Tests`.
- Inside that folder the structure mirrors the API controllers. For example:
  - Controller `CountryController` -> tests in `src/Playground.Tests/Api/Controller/CountryController/`.
  - Each controller action has its own test file named `<ActionName><Controller>Test.cs`.
- The namespace for all test classes is `Playground.Tests.Controllers`.

## Test class guidelines

- Use **xUnit** and place `global using Xunit;` in `src/Playground.Tests/Usings.cs`.
- Declare private fields for all mocks (`Mock<IMediator>`, `Mock<ILogger<T>>`, etc.) and the controller being tested.
- Initialize mocks and controller instances in the constructor of the test class.
- Follow the Arrange / Act / Assert pattern inside each `[Fact]` method.
  - Method names use Portuguese phrases such as `Metodo_QuandoCondicao_DeveRetornarResultado`.
  - Verify the specific `IActionResult` type (`OkObjectResult`, `BadRequestObjectResult`, etc.) and its `StatusCode`/`Value`.
- Use **Moq** for mocking dependencies.
- See `src/Playground.Tests/Api/Controller/Template/SampleControllerTestTemplate.cs` for a sample layout (wrapped in `#if false` so it does not compile).

## Workflow requirements

Whenever modifying or adding tests, run `dotnet build` and `dotnet test` on `src/Playground.Ecs.sln` to ensure the project compiles and tests pass.
