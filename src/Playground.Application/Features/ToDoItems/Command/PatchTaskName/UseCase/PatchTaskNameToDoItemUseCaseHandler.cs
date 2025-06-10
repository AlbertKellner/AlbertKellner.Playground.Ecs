using MediatR;
using Playground.Application.Features.ToDoItems.Command.PatchTaskName.Models;

namespace Playground.Application.Features.ToDoItems.Command.PatchTaskName.UseCase
{
    public class PatchTaskNameToDoItemUseCaseHandler : IRequestHandler<PatchTaskNameToDoItemCommand, PatchTaskNameToDoItemOutput>
    {
        public Task<PatchTaskNameToDoItemOutput> Handle(PatchTaskNameToDoItemCommand input, CancellationToken cancellationToken)
        {
            var result = new PatchTaskNameToDoItemOutput
            {
                Id = input.Id,
                Task = input.TaskName
            };
            return Task.FromResult(result);
        }
    }
}
