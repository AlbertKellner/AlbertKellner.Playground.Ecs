using Moq;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Playground.Controllers;

namespace Playground.Tests.Controllers
{
    public class SampleControllerTestTemplate
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<SampleController>> _mockLogger;
        private readonly SampleController _controller;

        // private readonly SomeCommand _input;
        // private readonly SomeOutput _output;

        public SampleControllerTestTemplate()
        {
            _mockMediator = new Mock<IMediator>();
            _mockLogger = new Mock<ILogger<SampleController>>();

            _controller = new SampleController(_mockMediator.Object, _mockLogger.Object);

            // _input = new SomeCommand { ... };
            // _output = new SomeOutput { ... };
        }

        [Fact]
        public async Task Metodo_QuandoCondicao_DeveRetornarResultado()
        {
            var input = /* instanciar entrada */ default(SomeCommand);
            var output = /* definir saída esperada */ default(SomeOutput);

            _mockMediator
                .Setup(mediator => mediator.Send(input, It.IsAny<CancellationToken>()))
                .ReturnsAsync(output);

            var actionResult = await _controller.MetodoAsync(input, CancellationToken.None);

            var response = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.Equal(output, response.Value);
        }
    }
}
