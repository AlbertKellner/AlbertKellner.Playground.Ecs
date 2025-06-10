using MediatR;
using Playground.Application.Features.ToDoItems.Command.Delete.Models;

namespace Playground.Application.Features.ToDoItems.Command.Delete.UseCase
{
    public class DeleteToDoItemUseCaseHandler : IRequestHandler<DeleteToDoItemCommand, DeleteToDoItemOutput>
    {
        public Task<DeleteToDoItemOutput> Handle(DeleteToDoItemCommand input, CancellationToken cancellationToken)
        {
            return Task.FromResult(new DeleteToDoItemOutput());
        }
    }
}
