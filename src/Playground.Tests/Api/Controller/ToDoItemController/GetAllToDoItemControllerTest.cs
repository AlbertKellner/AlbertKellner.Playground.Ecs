using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Playground.API.Controllers;
using Playground.Application.Features.ToDoItems.Query.GetAll.Models;
using Microsoft.AspNetCore.Http;

namespace Playground.Tests.Controllers
{
    public class GetAllToDoItemControllerTest
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<ToDoItemController>> _mockLogger;
        private readonly ToDoItemController _controller;

        public GetAllToDoItemControllerTest()
        {
            _mockMediator = new Mock<IMediator>();
            _mockLogger = new Mock<ILogger<ToDoItemController>>();

            _controller = new ToDoItemController(_mockMediator.Object, _mockLogger.Object);
        }

        [Fact(DisplayName = "GetAllAsync WhenItemsExist ShouldReturnOk")]
        public async Task GetAllAsync_WhenItemsExist_ShouldReturnOk()
        {
            var output = new List<GetAllToDoItemOutput>
            {
                new GetAllToDoItemOutput { Id = 1, Task = "Task 1", IsCompleted = false },
                new GetAllToDoItemOutput { Id = 2, Task = "Task 2", IsCompleted = true }
            };

            _mockMediator
                .Setup(mediator =>
                    mediator.Send(It.IsAny<GetAllToDoItemQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(output);

            var actionResult = await _controller.GetAllAsync(CancellationToken.None);

            var response = Assert.IsType<OkObjectResult>(actionResult);
            var responseData = Assert.IsType<List<GetAllToDoItemOutput>>(response.Value);

            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.Equal(output, responseData);
            _mockMediator.Verify(m =>
                m.Send(It.IsAny<GetAllToDoItemQuery>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact(DisplayName = "GetAllAsync WhenNoItemsExist ShouldReturnNoContent")]
        public async Task GetAllAsync_WhenNoItemsExist_ShouldReturnNoContent()
        {
            var output = new List<GetAllToDoItemOutput>();

            _mockMediator
                .Setup(mediator =>
                    mediator.Send(It.IsAny<GetAllToDoItemQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(output);

            var actionResult = await _controller.GetAllAsync(CancellationToken.None);

            var response = Assert.IsType<NoContentResult>(actionResult);

            Assert.Equal(StatusCodes.Status204NoContent, response.StatusCode);
            _mockMediator.Verify(m =>
                m.Send(It.IsAny<GetAllToDoItemQuery>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
