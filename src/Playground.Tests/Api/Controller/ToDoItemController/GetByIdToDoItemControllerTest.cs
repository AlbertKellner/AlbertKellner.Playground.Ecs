using Xunit;
using Moq;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Playground.API.Controllers;
using Playground.Application.Features.ToDoItems.Query.GetById.Models;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.Tests.Controllers
{
    public class GetByIdToDoItemControllerTest
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<ToDoItemController>> _mockLogger;
        private readonly ToDoItemController _controller;
        private readonly GetByIdToDoItemQuery _validInput;
        private readonly GetByIdToDoItemQuery _invalidInput;
        private readonly GetByIdToDoItemOutput _validOutput;
        private readonly GetByIdToDoItemOutput _invalidOutput;

        public GetByIdToDoItemControllerTest()
        {
            _mockMediator = new Mock<IMediator>();
            _mockLogger = new Mock<ILogger<ToDoItemController>>();

            _controller = new ToDoItemController(_mockMediator.Object, _mockLogger.Object);

            _validInput = new GetByIdToDoItemQuery();
            _validInput.SetId(1);

            _invalidInput = new GetByIdToDoItemQuery();
            _invalidInput.SetId(0);

            _validOutput = new GetByIdToDoItemOutput { Id = 1, Task = "Sample task", IsCompleted = false };
            _invalidOutput = new GetByIdToDoItemOutput { Id = 0, Task = string.Empty, IsCompleted = false };
        }

        [Fact(DisplayName = "GetByIdAsync QuandoEntradaEValida DeveRetornarOk")]
        public async Task GetByIdAsync_QuandoEntradaEValida_DeveRetornarOk()
        {
            _mockMediator
                .Setup(mediator =>
                    mediator.Send(_validInput, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_validOutput);

            var actionResult = await _controller.GetByIdAsync(_validInput.Id, _validInput, CancellationToken.None);

            var response = Assert.IsType<OkObjectResult>(actionResult);

            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.NotNull(response.Value);
            _mockMediator.Verify(m =>
                m.Send(_validInput, It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact(DisplayName = "GetByIdAsync QuandoEntradaEInvalida DeveRetornarBadRequest")]
        public async Task GetByIdAsync_QuandoEntradaEInvalida_DeveRetornarBadRequest()
        {
            _mockMediator
                .Setup(mediator =>
                    mediator.Send(_invalidInput, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_invalidOutput);

            var actionResult = await _controller.GetByIdAsync(_invalidInput.Id, _invalidInput, CancellationToken.None);

            var response = Assert.IsType<BadRequestObjectResult>(actionResult);

            Assert.Equal(StatusCodes.Status400BadRequest, response.StatusCode);
            Assert.NotNull(response.Value);
            _mockMediator.Verify(m =>
                m.Send(It.IsAny<GetByIdToDoItemQuery>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact(DisplayName = "GetByIdAsync QuandoOutputEInvalido DeveRetornarNoContent")]
        public async Task GetByIdAsync_QuandoOutputEInvalido_DeveRetornarNoContent()
        {
            _mockMediator
                .Setup(mediator =>
                    mediator.Send(_validInput, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_invalidOutput);

            var actionResult = await _controller.GetByIdAsync(_validInput.Id, _validInput, CancellationToken.None);

            var response = Assert.IsType<NoContentResult>(actionResult);

            Assert.Equal(StatusCodes.Status204NoContent, response.StatusCode);
            _mockMediator.Verify(m =>
                m.Send(_validInput, It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
