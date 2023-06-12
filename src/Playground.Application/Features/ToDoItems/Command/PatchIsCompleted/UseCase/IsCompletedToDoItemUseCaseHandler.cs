using MediatR;
using Playground.Application.Features.ToDoItems.Command.PatchIsCompleted.Models;

namespace Playground.Application.Features.ToDoItems.Command.PatchIsCompleted.UseCase
{
    public class IsCompletedToDoItemUseCaseHandler : IRequestHandler<IsCompletedToDoItemInput, IsCompletedToDoItemOutput>
    {
        public async Task<IsCompletedToDoItemOutput> Handle(IsCompletedToDoItemInput input, CancellationToken cancellationToken)
        {
            return new IsCompletedToDoItemOutput
            {
                Id = input.Id,
                IsCompleted = input.IsCompleted
            };
        }
    }
}
