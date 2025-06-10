using Moq;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Playground.Controllers.v2_0;
using Playground.Controllers;
using Playground.Application.Features.ToDoItems.Query.GetById.Models;

namespace Playground.Tests.Controllers
{
    public class GetByIdToDoItemsControllerTest
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<ToDoItemController>> _mockLogger;
        private readonly ToDoItemsController _controller;
        private readonly GetByIdToDoItemQuery _validInput;
        private readonly GetByIdToDoItemQuery _invalidInput;
        private readonly GetByIdToDoItemOutput _validOutput;
        private readonly GetByIdToDoItemOutput _invalidOutput;

        public GetByIdToDoItemsControllerTest()
        {
            _mockMediator = new Mock<IMediator>();
            _mockLogger = new Mock<ILogger<ToDoItemController>>();
            _controller = new ToDoItemsController(_mockMediator.Object, _mockLogger.Object);

            _validInput = new GetByIdToDoItemQuery();
            _validInput.SetId(1);

            _invalidInput = new GetByIdToDoItemQuery();
            _invalidInput.SetId(0);

            _validOutput = new GetByIdToDoItemOutput { Id = 1, Task = "Sample", IsCompleted = false };
            _invalidOutput = new GetByIdToDoItemOutput { Id = 0, Task = string.Empty, IsCompleted = false };
        }

        [Fact]
        public async Task GetByIdAsync_QuandoValido_DeveRetornarOk()
        {
            _mockMediator
                .Setup(m => m.Send(_validInput, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_validOutput);

            var result = await _controller.GetByIdAsync(_validInput.Id, _validInput, CancellationToken.None);

            var response = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.Equal(_validOutput, response.Value);
        }

        [Fact]
        public async Task GetByIdAsync_QuandoEntradaInvalida_DeveRetornarBadRequest()
        {
            _mockMediator
                .Setup(m => m.Send(_invalidInput, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_invalidOutput);

            var result = await _controller.GetByIdAsync(_invalidInput.Id, _invalidInput, CancellationToken.None);

            var response = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetByIdAsync_QuandoOutputInvalido_DeveRetornarNoContent()
        {
            _mockMediator
                .Setup(m => m.Send(_validInput, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_invalidOutput);

            var result = await _controller.GetByIdAsync(_validInput.Id, _validInput, CancellationToken.None);

            var response = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, response.StatusCode);
        }
    }
}
