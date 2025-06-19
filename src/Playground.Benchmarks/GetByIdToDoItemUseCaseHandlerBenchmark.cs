using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Playground.Application.Features.ToDoItems.Query.GetById.Models;
using Playground.Application.Features.ToDoItems.Query.GetById.UseCase;

namespace Playground.Benchmarks
{
    [MemoryDiagnoser]
    public class GetByIdToDoItemUseCaseHandlerBenchmark
    {
        private GetByIdToDoItemUseCaseHandler _handler = null!;
        private GetByIdToDoItemQuery _query = null!;

        [GlobalSetup]
        public void Setup()
        {
            _handler = new GetByIdToDoItemUseCaseHandler();
            _query = new GetByIdToDoItemQuery();
            _query.SetId(99);
        }

        [Benchmark]
        public async Task<GetByIdToDoItemOutput> HandleAsync()
        {
            return await _handler.Handle(_query, CancellationToken.None);
        }
    }
}
