using MediatR;
using Playground.Application.Features.ToDoItems.Create.Models;

namespace Playground.Application.Features.ToDoItems.Create.UseCase
{
    public class CreateToDoItemUseCaseHandler : IRequestHandler<CreateToDoItemInput, CreateToDoItemOutput>
    {
        public async Task<CreateToDoItemOutput> Handle(CreateToDoItemInput request, CancellationToken cancellationToken)
        {
            return new CreateToDoItemOutput
            {
                Id = 1,
                Task = request.Task,
                IsCompleted = false
            };
        }
    }
}
