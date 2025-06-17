using Playground.Application.Features.ToDoItems.Command.Delete.UseCase;
using Playground.Application.Features.ToDoItems.Command.Delete.Models;

namespace Playground.Tests.Controllers
{
    public class HandleDeleteToDoItemUseCaseHandlerTest
    {
        private readonly DeleteToDoItemUseCaseHandler _handler;

        public HandleDeleteToDoItemUseCaseHandlerTest()
        {
            _handler = new DeleteToDoItemUseCaseHandler();
        }

        [Fact(DisplayName = "Handle DeveRetornarOutputValido")]
        public async Task Handle_DeveRetornarOutputValido()
        {
            var command = new DeleteToDoItemCommand(1);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsValid());
        }
    }
}
