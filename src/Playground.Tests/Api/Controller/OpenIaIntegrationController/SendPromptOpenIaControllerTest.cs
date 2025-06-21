using Moq;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Playground.API.Controllers;
using Playground.Application.Features.OpenIaIntegration.Command.SendPrompt.Models;
using Microsoft.AspNetCore.Http;

namespace Playground.Tests.Controllers
{
    public class SendPromptOpenIaControllerTest
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<OpenIaIntegrationController>> _mockLogger;
        private readonly OpenIaIntegrationController _controller;
        private readonly SendPromptCommand _validInput;
        private readonly SendPromptCommand _invalidInput;
        private readonly SendPromptOutput _validOutput;

        public SendPromptOpenIaControllerTest()
        {
            _mockMediator = new Mock<IMediator>();
            _mockLogger = new Mock<ILogger<OpenIaIntegrationController>>();
            _controller = new OpenIaIntegrationController(_mockMediator.Object, _mockLogger.Object);

            _validInput = new SendPromptCommand { Prompt = "hi", Model = "gpt-3.5-turbo", Temperature = 0.7f };
            _invalidInput = new SendPromptCommand { Prompt = string.Empty, Model = "gpt-3.5-turbo", Temperature = 0.7f };
            _validOutput = new SendPromptOutput { Response = "hello" };
        }

        [Fact(DisplayName = "SendPromptAsync QuandoEntradaEValida DeveRetornarOk")]
        public async Task SendPromptAsync_QuandoEntradaEValida_DeveRetornarOk()
        {
            _mockMediator
                .Setup(mediator => mediator.Send(_validInput, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_validOutput);

            var actionResult = await _controller.SendPromptAsync(_validInput, CancellationToken.None);

            var response = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.Equal(_validOutput, response.Value);
            _mockMediator.Verify(m => m.Send(_validInput, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "SendPromptAsync QuandoEntradaEInvalida DeveRetornarBadRequest")]
        public async Task SendPromptAsync_QuandoEntradaEInvalida_DeveRetornarBadRequest()
        {
            var actionResult = await _controller.SendPromptAsync(_invalidInput, CancellationToken.None);

            var response = Assert.IsType<BadRequestObjectResult>(actionResult);
            Assert.Equal(StatusCodes.Status400BadRequest, response.StatusCode);
            _mockMediator.Verify(m => m.Send(It.IsAny<SendPromptCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
