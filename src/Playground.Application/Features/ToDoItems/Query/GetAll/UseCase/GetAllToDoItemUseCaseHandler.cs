using MediatR;
using Playground.Application.Features.ToDoItems.Query.GetAll.Models;

namespace Playground.Application.Features.ToDoItems.Query.GetAll.UseCase
{
    public class GetAllToDoItemUseCaseHandler : IRequestHandler<GetAllToDoItemQuery, IEnumerable<GetAllToDoItemOutput>>
    {
        public Task<IEnumerable<GetAllToDoItemOutput>> Handle(GetAllToDoItemQuery input, CancellationToken cancellationToken)
        {
            var items = new List<GetAllToDoItemOutput>
            {
                new GetAllToDoItemOutput
                {
                    Id = 98,
                    Task = "Task 98",
                    IsCompleted = true
                },
                new GetAllToDoItemOutput
                {
                    Id = 99,
                    Task = "Task 99",
                    IsCompleted = true
                }
            };

            return Task.FromResult<IEnumerable<GetAllToDoItemOutput>>(items);
        }
    }
}
