using MediatR;
using Playground.Application.Features.ToDoItems.Query.GetById.Models;

namespace Playground.Application.Features.ToDoItems.Query.GetById.UseCase
{
    public class GetByIdToDoItemUseCaseHandler : IRequestHandler<GetByIdToDoItemQuery, GetByIdToDoItemOutput>
    {
        private static readonly GetByIdToDoItemOutput CachedItem = new()
        {
            Id = 99,
            Task = "GetById - ToDoItem - UseCaseHandler",
            IsCompleted = true
        };

        public Task<GetByIdToDoItemOutput> Handle(GetByIdToDoItemQuery input, CancellationToken cancellationToken)
        {
            return Task.FromResult(input.Id == CachedItem.Id ? CachedItem : new GetByIdToDoItemOutput());
        }
    }
}
