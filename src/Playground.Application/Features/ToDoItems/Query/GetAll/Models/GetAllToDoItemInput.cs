using MediatR;

namespace Playground.Application.Features.ToDoItems.Query.GetAll.Models
{
    public class GetAllToDoItemInput : IRequest<IEnumerable<GetAllToDoItemOutput>>
    {
        public IEnumerable<string> ErrosList()
        {
            return new List<string>();
        }

        public bool IsInvalid() => ErrosList().Any();

        public string FormattedErrosList() => $"({string.Join("|", ErrosList())})";
    }
}
