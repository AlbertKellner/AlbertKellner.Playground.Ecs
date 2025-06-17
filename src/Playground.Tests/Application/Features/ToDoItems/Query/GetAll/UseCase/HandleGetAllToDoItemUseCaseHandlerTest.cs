using Playground.Application.Features.ToDoItems.Query.GetAll.UseCase;
using Playground.Application.Features.ToDoItems.Query.GetAll.Models;

namespace Playground.Tests.Controllers
{
    public class HandleGetAllToDoItemUseCaseHandlerTest
    {
        private readonly GetAllToDoItemUseCaseHandler _handler;

        public HandleGetAllToDoItemUseCaseHandlerTest()
        {
            _handler = new GetAllToDoItemUseCaseHandler();
        }

        [Fact(DisplayName = "Handle DeveRetornarListaPreenchida")]
        public async Task Handle_DeveRetornarListaPreenchida()
        {
            var result = await _handler.Handle(new GetAllToDoItemQuery(), CancellationToken.None);

            Assert.NotEmpty(result);
        }
    }
}
