using Moq;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Playground.Controllers;
using Playground.Application.Features.ToDoItems.Command.PatchTaskName.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Xunit;
using System.Threading;

namespace Playground.Tests.Controllers
{
    public class PatchTaskNameToDoItemControllerTest
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<ToDoItemController>> _mockLogger;
        private readonly ToDoItemController _controller;
        private readonly PatchTaskNameToDoItemCommand _validInput;
        private readonly PatchTaskNameToDoItemCommand _invalidInput;
        private readonly PatchTaskNameToDoItemOutput _validOutput;
        private readonly PatchTaskNameToDoItemOutput _invalidOutput;

        public PatchTaskNameToDoItemControllerTest()
        {
            _mockMediator = new Mock<IMediator>();
            _mockLogger = new Mock<ILogger<ToDoItemController>>();

            _controller = new ToDoItemController(_mockMediator.Object, _mockLogger.Object);

            _validInput = new PatchTaskNameToDoItemCommand
            {
                Id = 1,
                TaskName = "Sample task"
            };

            _invalidInput = new PatchTaskNameToDoItemCommand
            { 
                Id = 0,
                TaskName = string.Empty
            };

            _validOutput = new PatchTaskNameToDoItemOutput { Id = 1, Task = "Sample task", IsCompleted = false };

            _invalidOutput = new PatchTaskNameToDoItemOutput();
        }

        [Fact]
        public async Task PatchTaskNameAsync_QuandoEntradaEValida_DeveRetornarOk()
        {
            _mockMediator
                .Setup(mediator =>
                    mediator.Send(_validInput, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_validOutput);

            var actionResult = await _controller.PatchTaskNameAsync(_validInput.Id, _validInput.TaskName, _validInput, CancellationToken.None);

            var response = Assert.IsType<OkResult>(actionResult);

            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
        }

        [Fact]
        public async Task PatchTaskNameAsync_QuandoEntradaEInvalidaEFezUpdate_DeveRetornarBadRequest()
        {
            _mockMediator
                .Setup(mediator =>
                    mediator.Send(_invalidInput, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_validOutput);

            var actionResult = await _controller.PatchTaskNameAsync(_invalidInput.Id, _invalidInput.TaskName, _invalidInput, CancellationToken.None);

            var response = Assert.IsType<BadRequestObjectResult>(actionResult);

            Assert.Equal(StatusCodes.Status400BadRequest, response.StatusCode);
            Assert.NotNull(response.Value);
        }

        [Fact]
        public async Task PatchTaskNameAsync_QuandoEntradaEValidaENaoFezUpdate_DeveRetornarNoContent()
        {
            _mockMediator
                .Setup(mediator =>
                    mediator.Send(_validInput, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_invalidOutput);

            var actionResult = await _controller.PatchTaskNameAsync(_validInput.Id, _validInput.TaskName, _validInput, CancellationToken.None);

            var response = Assert.IsType<NoContentResult>(actionResult);

            Assert.Equal(StatusCodes.Status204NoContent, response.StatusCode);
        }
    }
}
