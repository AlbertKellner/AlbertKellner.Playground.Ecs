using MediatR;
using Playground.Application.Features.ToDoItems.Command.PatchIsCompleted.Models;

namespace Playground.Application.Features.ToDoItems.Command.PatchIsCompleted.UseCase
{
    public class IsCompletedToDoItemUseCaseHandler : IRequestHandler<IsCompletedToDoItemCommand, IsCompletedToDoItemOutput>
    {
        public async Task<IsCompletedToDoItemOutput> Handle(IsCompletedToDoItemCommand input, CancellationToken cancellationToken)
        {
            return new IsCompletedToDoItemOutput
            {
                Id = input.Id,
                IsCompleted = input.IsCompleted
            };
        }
    }
}
