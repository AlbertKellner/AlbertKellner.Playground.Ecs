using Playground.Application.Features.ToDoItems.Command.Create.Models;

namespace Playground.Application.Features.ToDoItems.Command.Create.Interface
{
    public interface ICreateTodoItemRepository
    {
        Task<CreateToDoItemOutput> CreateToDoItemAsync(CreateToDoItemInput input, CancellationToken cancellationToken);
    }
}
