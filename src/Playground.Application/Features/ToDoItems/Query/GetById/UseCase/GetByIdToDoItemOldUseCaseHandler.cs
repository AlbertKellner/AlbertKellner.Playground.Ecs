using MediatR;
using Playground.Application.Features.ToDoItems.Query.GetById.Models;

namespace Playground.Application.Features.ToDoItems.Query.GetById.UseCase
{
    public class GetByIdToDoItemOldUseCaseHandler : IRequestHandler<GetByIdToDoItemOldQuery, GetByIdToDoItemOldOutput>
    {
        public Task<GetByIdToDoItemOldOutput> Handle(GetByIdToDoItemOldQuery input, CancellationToken cancellationToken)
        {
            var items = new List<GetByIdToDoItemOldOutput>
            {
                new GetByIdToDoItemOldOutput
                {
                    Id = 99,
                    Task = "GetById - ToDoItemOld - UseCaseHandler",
                    IsCompleted = true
                }
            };

            var result = items.SingleOrDefault(item => item.Id == input.Id) ?? new GetByIdToDoItemOldOutput();
            return Task.FromResult(result);
        }
    }
}
