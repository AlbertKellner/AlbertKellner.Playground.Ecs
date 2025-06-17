using Playground.Application.Features.ToDoItems.Command.PatchTaskName.UseCase;
using Playground.Application.Features.ToDoItems.Command.PatchTaskName.Models;

namespace Playground.Tests.Controllers
{
    public class HandlePatchTaskNameToDoItemUseCaseHandlerTest
    {
        private readonly PatchTaskNameToDoItemUseCaseHandler _handler;

        public HandlePatchTaskNameToDoItemUseCaseHandlerTest()
        {
            _handler = new PatchTaskNameToDoItemUseCaseHandler();
        }

        [Fact(DisplayName = "Handle DeveRetornarOutputComMesmoId")]
        public async Task Handle_DeveRetornarOutputComMesmoId()
        {
            var command = new PatchTaskNameToDoItemCommand { Id = 7, TaskName = "do" };

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Equal(7, result.Id);
            Assert.Equal("do", result.Task);
        }
    }
}
