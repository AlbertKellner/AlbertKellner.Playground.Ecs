using Moq;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Playground.API.Controllers;
using Playground.Application.Features.ToDoItems.Command.Create.Models;
using Microsoft.AspNetCore.Http;

namespace Playground.Tests.Controllers
{
    public class CreateToDoItemControllerTest
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<ToDoItemController>> _mockLogger;
        private readonly ToDoItemController _controller;
        private readonly CreateToDoItemCommand _validInput;
        private readonly CreateToDoItemCommand _invalidInput;
        private readonly CreateToDoItemOutput _validOutput;

        public CreateToDoItemControllerTest()
        {
            _mockMediator = new Mock<IMediator>();
            _mockLogger = new Mock<ILogger<ToDoItemController>>();

            _controller = new ToDoItemController(_mockMediator.Object, _mockLogger.Object);

            _validInput = new CreateToDoItemCommand { Task = "Sample task", IsCompleted = false };
            _invalidInput = new CreateToDoItemCommand { Task = "", IsCompleted = false };

            _validOutput = new CreateToDoItemOutput { Id = 1, Task = "Sample task", IsCompleted = false };
        }

        [Fact(DisplayName = "CreateAsync QuandoEntradaEValida DeveRetornarCriado")]
        public async Task CreateAsync_QuandoEntradaEValida_DeveRetornarCriado()
        {
            _mockMediator
                .Setup(mediator =>
                    mediator.Send(_validInput, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_validOutput);

            var actionResult = await _controller.CreateAsync(_validInput, CancellationToken.None);

            var response = Assert.IsType<CreatedAtRouteResult>(actionResult);

            Assert.Equal(StatusCodes.Status201Created, response.StatusCode);
            Assert.Equal(_validOutput, response.Value);
            _mockMediator.Verify(m =>
                m.Send(_validInput, It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact(DisplayName = "CreateAsync QuandoEntradaEInvalida DeveRetornarBadRequest")]
        public async Task CreateAsync_QuandoEntradaEInvalida_DeveRetornarBadRequest()
        {
            _mockMediator
                .Setup(mediator =>
                    mediator.Send(_invalidInput, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_validOutput);

            var actionResult = await _controller.CreateAsync(_invalidInput, CancellationToken.None);

            var response = Assert.IsType<BadRequestObjectResult>(actionResult);

            Assert.Equal(StatusCodes.Status400BadRequest, response.StatusCode);
            Assert.NotNull(response.Value);
            _mockMediator.Verify(m =>
                m.Send(It.IsAny<CreateToDoItemCommand>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
}
