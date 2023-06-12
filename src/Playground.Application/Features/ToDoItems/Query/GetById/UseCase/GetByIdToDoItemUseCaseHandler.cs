using MediatR;
using Playground.Application.Features.ToDoItems.Query.GetById.Models;

namespace Playground.Application.Features.ToDoItems.Query.GetById.UseCase
{
    public class GetByIdToDoItemUseCaseHandler : IRequestHandler<GetByIdToDoItemInput, GetByIdToDoItemOutput>
    {
        public async Task<GetByIdToDoItemOutput> Handle(GetByIdToDoItemInput input, CancellationToken cancellationToken)
        {
            var items = new List<GetByIdToDoItemOutput>
            {
                new GetByIdToDoItemOutput
                {
                    Id = 99,
                    Task = "GetById - ToDoItem - UseCaseHandler",
                    IsCompleted = true
                }
            };

            return items.SingleOrDefault(item => item.Id == input.Id) ?? new GetByIdToDoItemOutput();
        }
    }
}
