using MediatR;
using Playground.Application.Features.ToDoItems.Command.PatchTaskName.Models;

namespace Playground.Application.Features.ToDoItems.Command.PatchTaskName.UseCase
{
    public class PatchTaskNameToDoItemUseCaseHandler : IRequestHandler<PatchTaskNameToDoItemInput, PatchTaskNameToDoItemOutput>
    {
        public async Task<PatchTaskNameToDoItemOutput> Handle(PatchTaskNameToDoItemInput input, CancellationToken cancellationToken)
        {
            return new PatchTaskNameToDoItemOutput
            {
                Id = input.Id,
                Task = input.Task
            };
        }
    }
}
