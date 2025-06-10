using MediatR;
using Playground.Application.Features.ToDoItems.Query.GetById.Models;

namespace Playground.Application.Features.ToDoItems.Query.GetById.UseCase
{
    public class GetByIdToDoItemUseCaseHandler : IRequestHandler<GetByIdToDoItemQuery, GetByIdToDoItemOutput>
    {
        public Task<GetByIdToDoItemOutput> Handle(GetByIdToDoItemQuery input, CancellationToken cancellationToken)
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

            var result = items.SingleOrDefault(item => item.Id == input.Id) ?? new GetByIdToDoItemOutput();
            return Task.FromResult(result);
        }
    }
}
