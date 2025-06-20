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

        private static readonly GetByIdToDoItemOutput EmptyItem = new();

        private static readonly Task<GetByIdToDoItemOutput> CachedTask = Task.FromResult(CachedItem);
        private static readonly Task<GetByIdToDoItemOutput> EmptyTask = Task.FromResult(EmptyItem);

        public Task<GetByIdToDoItemOutput> Handle(GetByIdToDoItemQuery input, CancellationToken cancellationToken)
        {
            return input.Id == CachedItem.Id ? CachedTask : EmptyTask;
        }
    }
}
