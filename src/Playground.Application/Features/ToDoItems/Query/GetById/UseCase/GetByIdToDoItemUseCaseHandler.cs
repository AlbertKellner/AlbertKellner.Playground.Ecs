using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Playground.Application.Features.ToDoItems.Query.GetById.Models;

namespace Playground.Application.Features.ToDoItems.Query.GetById.UseCase
{
    public class GetByIdToDoItemUseCaseHandler : IRequestHandler<GetByIdToDoItemQuery, GetByIdToDoItemOutput>
    {
        private static readonly MemoryCache MemoryCache = new(new MemoryCacheOptions());

        private static readonly GetByIdToDoItemOutput CachedItem = new()
        {
            Id = 99,
            Task = "GetById - ToDoItem - UseCaseHandler",
            IsCompleted = true
        };

        private const string CacheKey = "GetByIdToDoItem";

        public Task<GetByIdToDoItemOutput> Handle(GetByIdToDoItemQuery input, CancellationToken cancellationToken)
        {
            if (input.Id != CachedItem.Id)
                return Task.FromResult(new GetByIdToDoItemOutput());

            if (!MemoryCache.TryGetValue(CacheKey, out GetByIdToDoItemOutput? item))
            {
                item = CachedItem;
                MemoryCache.Set(CacheKey, item, TimeSpan.FromMinutes(1));
            }

            return Task.FromResult(item!);
        }
    }
}
