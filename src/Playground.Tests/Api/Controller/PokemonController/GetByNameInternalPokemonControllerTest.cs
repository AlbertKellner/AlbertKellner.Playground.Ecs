using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Playground.Application.Features.Pokemon.Endpoint;
using Playground.Application.Shared.Domain.ApiDto;

namespace Playground.Tests.Controllers;

public class GetByNameInternalPokemonControllerTest
{
    private readonly Mock<ILoggerFactory> _mockLoggerFactory;
    private readonly Mock<ILogger> _mockLogger;

    public GetByNameInternalPokemonControllerTest()
    {
        _mockLoggerFactory = new Mock<ILoggerFactory>();
        _mockLogger = new Mock<ILogger>();
        _mockLoggerFactory.Setup(l => l.CreateLogger(It.IsAny<string>()))
            .Returns(_mockLogger.Object);
    }

    [Fact(DisplayName = "Handle QuandoPikachu DeveRetornarOk")]
    public void Handle_QuandoPikachu_DeveRetornarOk()
    {
        var result = GetByNameInternalEndpoint.Handle(
            "pikachu",
            _mockLoggerFactory.Object);

        var okResult = Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Ok<PokemonOutApiDto>>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        var pokemon = Assert.IsType<PokemonOutApiDto>(okResult.Value);
        Assert.Equal("pikachu", pokemon.Name);
    }

    [Fact(DisplayName = "Handle QuandoNaoForPikachu DeveRetornarNoContent")]
    public void Handle_QuandoNaoForPikachu_DeveRetornarNoContent()
    {
        var result = GetByNameInternalEndpoint.Handle(
            "mew",
            _mockLoggerFactory.Object);

        var noContent = Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.NoContent>(result);
        Assert.Equal(StatusCodes.Status204NoContent, noContent.StatusCode);
    }
}
