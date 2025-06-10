using Playground.Application.Features.ToDoItems.Command.Update.UseCase;
using Playground.Application.Features.ToDoItems.Command.Update.Models;

namespace Playground.Tests.Controllers
{
    public class HandleUpdateToDoItemUseCaseHandlerTest
    {
        private readonly UpdateToDoItemUseCaseHandler _handler;

        public HandleUpdateToDoItemUseCaseHandlerTest()
        {
            _handler = new UpdateToDoItemUseCaseHandler();
        }

        [Fact]
        public async Task Handle_DeveRetornarOutputComMesmoId()
        {
            var command = new UpdateToDoItemCommand { Task = "task", IsCompleted = true };
            command.SetId(2);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Equal(2, result.Id);
            Assert.Equal("task", result.Task);
            Assert.True(result.IsCompleted);
        }
    }
}
