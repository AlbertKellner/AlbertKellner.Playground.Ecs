using MediatR;
using Playground.Application.Features.ToDoItems.Command.PatchTaskName.Models;

namespace Playground.Application.Features.ToDoItems.Command.PatchTaskName.UseCase
{
    public class PatchTaskNameToDoItemUseCaseHandler : IRequestHandler<PatchTaskNameToDoItemCommand, PatchTaskNameToDoItemOutput>
    {
        public async Task<PatchTaskNameToDoItemOutput> Handle(PatchTaskNameToDoItemCommand input, CancellationToken cancellationToken)
        {
            return new PatchTaskNameToDoItemOutput
            {
                Id = input.Id,
                Task = input.TaskName
            };
        }
    }
}
