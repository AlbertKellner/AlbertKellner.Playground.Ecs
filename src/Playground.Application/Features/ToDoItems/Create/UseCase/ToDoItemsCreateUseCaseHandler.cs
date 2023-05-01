using MediatR;
using Playground.Application.Features.ToDoItems.Create.Models;

namespace Playground.Application.Features.ToDoItems.Create.UseCase
{
    public class ToDoItemsCreateUseCaseHandler : IRequestHandler<CreateInput, CreateOutput>
    {

        public async Task<CreateOutput> Handle(CreateInput request, CancellationToken cancellationToken)
        {
            return new CreateOutput();
        }
    }
}
