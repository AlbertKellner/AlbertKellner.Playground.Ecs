using MediatR;

namespace Playground.Application.Features.ToDoItems.Create.Models
{
    public class CreateInput : IRequest<CreateOutput>
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public bool IsCompleted { get; set; }
    }
}
