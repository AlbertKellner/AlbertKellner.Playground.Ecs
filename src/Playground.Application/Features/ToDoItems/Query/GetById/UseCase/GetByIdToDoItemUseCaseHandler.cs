using MediatR;
using Playground.Application.Features.ToDoItems.Query.GetById.Models;

namespace Playground.Application.Features.ToDoItems.Query.GetById.UseCase
{
    public class GetByIdToDoItemUseCaseHandler : IRequestHandler<GetByIdToDoItemQuery, GetByIdToDoItemOutput>
    {
        private static readonly Dictionary<long, GetByIdToDoItemOutput> _items = new()
        {
            [99] = new GetByIdToDoItemOutput
            {
                Id = 99,
                Task = "GetById - ToDoItem - UseCaseHandler",
                IsCompleted = true
            }
        };

        public Task<GetByIdToDoItemOutput> Handle(GetByIdToDoItemQuery input, CancellationToken cancellationToken)
        {
            _items.TryGetValue(input.Id, out var result);
            return Task.FromResult(result ?? new GetByIdToDoItemOutput());
        }
    }
}
