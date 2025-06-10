using MediatR;
using Playground.Application.Features.ToDoItems.Command.PatchIsCompleted.Models;

namespace Playground.Application.Features.ToDoItems.Command.PatchIsCompleted.UseCase
{
    public class IsCompletedToDoItemUseCaseHandler : IRequestHandler<IsCompletedToDoItemCommand, IsCompletedToDoItemOutput>
    {
        public Task<IsCompletedToDoItemOutput> Handle(IsCompletedToDoItemCommand input, CancellationToken cancellationToken)
        {
            var result = new IsCompletedToDoItemOutput
            {
                Id = input.Id,
                IsCompleted = input.IsCompleted
            };
            return Task.FromResult(result);
        }
    }
}
