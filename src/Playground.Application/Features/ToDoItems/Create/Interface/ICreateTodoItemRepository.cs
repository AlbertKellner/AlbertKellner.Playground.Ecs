using Playground.Application.Features.ToDoItems.Create.Models;

namespace Playground.Application.Features.ToDoItems.Create.Interface
{
    public interface ICreateTodoItemRepository
    {
        Task<CreateToDoItemOutput> CreateToDoItemAsync(CreateToDoItemInput input, CancellationToken cancellationToken);
    }
}
