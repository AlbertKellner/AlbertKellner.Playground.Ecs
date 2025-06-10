using MediatR;
using Playground.Application.Features.ToDoItems.Command.Update.Models;

namespace Playground.Application.Features.ToDoItems.Command.Update.UseCase
{
    public class UpdateToDoItemUseCaseHandler : IRequestHandler<UpdateToDoItemCommand, UpdateToDoItemOutput>
    {
        public async Task<UpdateToDoItemOutput> Handle(UpdateToDoItemCommand input, CancellationToken cancellationToken)
        {
            return new UpdateToDoItemOutput
            {
                Id = input.Id,
                Task = input.Task,
                IsCompleted = input.IsCompleted
            };
        }
    }
}
