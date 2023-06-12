using MediatR;
using Playground.Application.Features.ToDoItems.Command.Create.Interface;
using Playground.Application.Features.ToDoItems.Command.Create.Models;

namespace Playground.Application.Features.ToDoItems.Command.Create.UseCase
{
    public class CreateToDoItemUseCaseHandler : IRequestHandler<CreateToDoItemCommand, CreateToDoItemOutput>
    {
        private readonly ICreateTodoItemRepository _createTodoItemRepository;

        public CreateToDoItemUseCaseHandler(ICreateTodoItemRepository createTodoItemRepository)
        {
            _createTodoItemRepository = createTodoItemRepository;
        }

        public async Task<CreateToDoItemOutput> Handle(CreateToDoItemCommand input, CancellationToken cancellationToken)
        {
            var result = await _createTodoItemRepository.CreateToDoItemAsync(input, cancellationToken);

            return result;
        }
    }
}
