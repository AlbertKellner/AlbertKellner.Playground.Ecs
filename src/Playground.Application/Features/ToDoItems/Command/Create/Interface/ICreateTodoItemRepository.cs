using Playground.Application.Features.ToDoItems.Command.Create.Models;

namespace Playground.Application.Features.ToDoItems.Command.Create.Interface
{
    public interface ICreateTodoItemRepository
    {
        Task<CreateToDoItemOutput> CreateToDoItemAsync(CreateToDoItemCommand input, CancellationToken cancellationToken);
    }
}
