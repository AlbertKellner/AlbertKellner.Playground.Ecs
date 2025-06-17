using Moq;
using Playground.Application.Features.ToDoItems.Command.Create.UseCase;
using Playground.Application.Features.ToDoItems.Command.Create.Interface;
using Playground.Application.Features.ToDoItems.Command.Create.Models;

namespace Playground.Tests.Controllers
{
    public class HandleCreateToDoItemUseCaseHandlerTest
    {
        private readonly Mock<ICreateTodoItemRepository> _mockRepo;
        private readonly CreateToDoItemUseCaseHandler _handler;

        public HandleCreateToDoItemUseCaseHandlerTest()
        {
            _mockRepo = new Mock<ICreateTodoItemRepository>();
            _handler = new CreateToDoItemUseCaseHandler(_mockRepo.Object);
        }

        [Fact(DisplayName = "Handle QuandoExecutado DeveRetornarItemCriado")]
        public async Task Handle_QuandoExecutado_DeveRetornarItemCriado()
        {
            var command = new CreateToDoItemCommand { Task = "task" };
            var expected = new CreateToDoItemOutput { Id = 1, Task = "task", IsCompleted = false };

            _mockRepo
                .Setup(r => r.CreateToDoItemAsync(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Equal(expected, result);
            _mockRepo.Verify(r => r.CreateToDoItemAsync(command, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
