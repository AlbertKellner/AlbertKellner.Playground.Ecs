using Moq;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Playground.Controllers;
using Playground.Application.Features.ToDoItems.Command.Update.Models;

namespace Playground.Tests.Controllers
{
    public class UpdateToDoItemControllerTest
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<ToDoItemController>> _mockLogger;
        private readonly ToDoItemController _controller;
        private readonly UpdateToDoItemCommand _validInput;
        private readonly UpdateToDoItemCommand _invalidInput;
        private readonly UpdateToDoItemOutput _validOutput;

        public UpdateToDoItemControllerTest()
        {
            _mockMediator = new Mock<IMediator>();
            _mockLogger = new Mock<ILogger<ToDoItemController>>();
            _controller = new ToDoItemController(_mockMediator.Object, _mockLogger.Object);

            _validInput = new UpdateToDoItemCommand { Task = "Sample task", IsCompleted = false };
            _validInput.SetId(1);

            _invalidInput = new UpdateToDoItemCommand { Task = "", IsCompleted = false };
            _invalidInput.SetId(0);

            _validOutput = new UpdateToDoItemOutput { Id = 1, Task = "Sample task", IsCompleted = false };
        }

        [Fact]
        public async Task UpdateAsync_QuandoEntradaEValida_DeveRetornarOk()
        {
            _mockMediator
                .Setup(mediator =>
                    mediator.Send(_validInput, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_validOutput);

            var actionResult = await _controller.UpdateAsync(_validInput.Id, _validInput, CancellationToken.None);

            var response = Assert.IsType<OkResult>(actionResult);

            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            _mockMediator.Verify(m =>
                m.Send(_validInput, It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_QuandoEntradaEInvalida_DeveRetornarBadRequest()
        {
            _mockMediator
                .Setup(mediator =>
                    mediator.Send(_invalidInput, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_validOutput);

            var actionResult = await _controller.UpdateAsync(_invalidInput.Id, _invalidInput, CancellationToken.None);

            var response = Assert.IsType<BadRequestObjectResult>(actionResult);

            Assert.Equal(StatusCodes.Status400BadRequest, response.StatusCode);
            Assert.NotNull(response.Value);
            _mockMediator.Verify(m =>
                m.Send(It.IsAny<UpdateToDoItemCommand>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
}
