using Playground.Application.Features.ToDoItems.Command.PatchIsCompleted.UseCase;
using Playground.Application.Features.ToDoItems.Command.PatchIsCompleted.Models;

namespace Playground.Tests.Controllers
{
    public class HandleIsCompletedToDoItemUseCaseHandlerTest
    {
        private readonly IsCompletedToDoItemUseCaseHandler _handler;

        public HandleIsCompletedToDoItemUseCaseHandlerTest()
        {
            _handler = new IsCompletedToDoItemUseCaseHandler();
        }

        [Fact]
        public async Task Handle_QuandoExecutado_DeveRetornarMesmoId()
        {
            var command = new IsCompletedToDoItemCommand { Id = 1, IsCompleted = true };

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Equal(command.Id, result.Id);
            Assert.True(result.IsCompleted);
        }
    }
}
