using Moq;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Playground.Application.Features.Pokemon.GetByName.Models;
using Playground.Application.Features.Pokemon.GetByName.Endpoint;

namespace Playground.Tests.Controllers;

public class GetByNameExternalPokemonControllerTest
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly Mock<ILoggerFactory> _mockLoggerFactory;
    private readonly Mock<ILogger> _mockLogger;
    private readonly GetByNamePokemonQuery _validInput;
    private readonly GetByNamePokemonQuery _invalidInput;
    private readonly GetByNamePokemonOutput _validOutput;
    private readonly GetByNamePokemonOutput _invalidOutput;

    public GetByNameExternalPokemonControllerTest()
    {
        _mockMediator = new Mock<IMediator>();
        _mockLoggerFactory = new Mock<ILoggerFactory>();
        _mockLogger = new Mock<ILogger>();
        _mockLoggerFactory.Setup(l => l.CreateLogger(It.IsAny<string>()))
            .Returns(_mockLogger.Object);

        _validInput = new GetByNamePokemonQuery();
        _validInput.SetName("pikachu");

        _invalidInput = new GetByNamePokemonQuery();
        _invalidInput.SetName(string.Empty);

        _validOutput = new GetByNamePokemonOutput
        {
            Name = "pikachu",
            BaseExperience = 100,
            LocationAreaEncounters = "Forest"
        };

        _invalidOutput = new GetByNamePokemonOutput();
    }

    [Fact(DisplayName = "HandleAsync QuandoEntradaValida DeveRetornarOk")]
    public async Task HandleAsync_QuandoEntradaValida_DeveRetornarOk()
    {
        _mockMediator
            .Setup(m => m.Send(_validInput, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_validOutput);

        var result = await GetByNameExternalEndpoint.HandleAsync(
            _validInput.Name,
            _validInput,
            _mockMediator.Object,
            _mockLoggerFactory.Object,
            CancellationToken.None);

        var okResult = Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Ok<GetByNamePokemonOutput>>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        Assert.Equal(_validOutput, okResult.Value);
        _mockMediator.Verify(m =>
            m.Send(_validInput, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact(DisplayName = "HandleAsync QuandoEntradaInvalida DeveRetornarBadRequest")]
    public async Task HandleAsync_QuandoEntradaInvalida_DeveRetornarBadRequest()
    {
        var result = await GetByNameExternalEndpoint.HandleAsync(
            _invalidInput.Name,
            _invalidInput,
            _mockMediator.Object,
            _mockLoggerFactory.Object,
            CancellationToken.None);

        var badRequest = Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.BadRequest<IEnumerable<string>>>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequest.StatusCode);
        _mockMediator.Verify(m =>
            m.Send(It.IsAny<GetByNamePokemonQuery>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact(DisplayName = "HandleAsync QuandoOutputInvalido DeveRetornarNoContent")]
    public async Task HandleAsync_QuandoOutputInvalido_DeveRetornarNoContent()
    {
        _mockMediator
            .Setup(m => m.Send(_validInput, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_invalidOutput);

        var result = await GetByNameExternalEndpoint.HandleAsync(
            _validInput.Name,
            _validInput,
            _mockMediator.Object,
            _mockLoggerFactory.Object,
            CancellationToken.None);

        var noContent = Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.NoContent>(result);
        Assert.Equal(StatusCodes.Status204NoContent, noContent.StatusCode);
        _mockMediator.Verify(m =>
            m.Send(_validInput, It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
