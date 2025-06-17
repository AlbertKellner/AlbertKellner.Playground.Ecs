using Playground.Application.Features.ToDoItems.Query.GetById.UseCase;
using Playground.Application.Features.ToDoItems.Query.GetById.Models;

namespace Playground.Tests.Controllers
{
    public class HandleGetByIdToDoItemUseCaseHandlerTest
    {
        private readonly GetByIdToDoItemUseCaseHandler _handler;

        public HandleGetByIdToDoItemUseCaseHandlerTest()
        {
            _handler = new GetByIdToDoItemUseCaseHandler();
        }

        [Fact(DisplayName = "Handle QuandoIdExistente DeveRetornarItem")]
        public async Task Handle_QuandoIdExistente_DeveRetornarItem()
        {
            var query = new GetByIdToDoItemQuery();
            query.SetId(99);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Equal(99, result.Id);
            Assert.NotEmpty(result.Task);
        }

        [Fact(DisplayName = "Handle QuandoIdNaoExistente DeveRetornarVazio")]
        public async Task Handle_QuandoIdNaoExistente_DeveRetornarVazio()
        {
            var query = new GetByIdToDoItemQuery();
            query.SetId(1);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Equal(0, result.Id);
        }
    }
}
