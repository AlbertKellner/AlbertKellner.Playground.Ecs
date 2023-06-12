using MediatR;
using Playground.Application.Features.ToDoItems.Command.Delete.Models;

namespace Playground.Application.Features.ToDoItems.Command.Delete.UseCase
{
    public class DeleteToDoItemUseCaseHandler : IRequestHandler<DeleteToDoItemInput, DeleteToDoItemOutput>
    {
        public async Task<DeleteToDoItemOutput> Handle(DeleteToDoItemInput input, CancellationToken cancellationToken)
        {
            return new DeleteToDoItemOutput
            {
                IsDeleted = true
            };
        }
    }
}
