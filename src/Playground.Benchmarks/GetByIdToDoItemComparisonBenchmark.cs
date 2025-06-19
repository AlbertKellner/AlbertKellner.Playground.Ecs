using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Playground.Application.Features.ToDoItems.Query.GetById.Models;
using Playground.Application.Features.ToDoItems.Query.GetById.UseCase;

namespace Playground.Benchmarks
{
    [MemoryDiagnoser]
    public class GetByIdToDoItemComparisonBenchmark
    {
        private GetByIdToDoItemUseCaseHandler _newHandler = null!;
        private GetByIdToDoItemOldUseCaseHandler _oldHandler = null!;
        private GetByIdToDoItemQuery _newQuery = null!;
        private GetByIdToDoItemOldQuery _oldQuery = null!;

        [GlobalSetup]
        public void Setup()
        {
            _newHandler = new GetByIdToDoItemUseCaseHandler();
            _newQuery = new GetByIdToDoItemQuery();
            _newQuery.SetId(99);

            _oldHandler = new GetByIdToDoItemOldUseCaseHandler();
            _oldQuery = new GetByIdToDoItemOldQuery();
            _oldQuery.SetId(99);
        }

        [Benchmark]
        public async Task<GetByIdToDoItemOutput> NewHandlerBenchmark()
        {
            return await _newHandler.Handle(_newQuery, CancellationToken.None);
        }

        [Benchmark]
        public async Task<GetByIdToDoItemOldOutput> OldHandlerBenchmark()
        {
            return await _oldHandler.Handle(_oldQuery, CancellationToken.None);
        }
    }
}
