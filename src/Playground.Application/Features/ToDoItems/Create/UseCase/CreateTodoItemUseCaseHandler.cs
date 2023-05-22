using MediatR;
using Playground.Application.Features.ToDoItems.Create.Interface;
using Playground.Application.Features.ToDoItems.Create.Models;

namespace Playground.Application.Features.ToDoItems.Create.UseCase
{
    public class CreateToDoItemUseCaseHandler : IRequestHandler<CreateToDoItemInput, CreateToDoItemOutput>
    {
        private readonly ICreateTodoItemRepository _createTodoItemRepository;

        public CreateToDoItemUseCaseHandler(ICreateTodoItemRepository createTodoItemRepository)
        {
            _createTodoItemRepository = createTodoItemRepository;
        }

        public async Task<CreateToDoItemOutput> Handle(CreateToDoItemInput input, CancellationToken cancellationToken)
        {
            var result = await _createTodoItemRepository.CreateToDoItemAsync(input, cancellationToken);

            return result;
        }
    }
}
