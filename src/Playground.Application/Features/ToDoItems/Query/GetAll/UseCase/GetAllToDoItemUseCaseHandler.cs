using MediatR;
using Playground.Application.Features.ToDoItems.Query.GetAll.Models;

namespace Playground.Application.Features.ToDoItems.Query.GetAll.UseCase
{
    public class GetAllToDoItemUseCaseHandler : IRequestHandler<GetAllToDoItemInput, IEnumerable<GetAllToDoItemOutput>>
    {
        public async Task<IEnumerable<GetAllToDoItemOutput>> Handle(GetAllToDoItemInput input, CancellationToken cancellationToken)
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

            return items;
        }
    }
}
