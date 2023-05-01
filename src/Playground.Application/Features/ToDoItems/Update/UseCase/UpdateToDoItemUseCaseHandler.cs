using MediatR;
using Playground.Application.Features.ToDoItems.Update.Models;

namespace Playground.Application.Features.ToDoItems.Update.UseCase
{
    public class UpdateToDoItemUseCaseHandler : IRequestHandler<UpdateToDoItemInput, UpdateToDoItemOutput>
    {
        public async Task<UpdateToDoItemOutput> Handle(UpdateToDoItemInput input, CancellationToken cancellationToken)
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
