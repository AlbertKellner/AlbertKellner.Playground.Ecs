using Playground.Application.Features.ToDoItems.IsCompleted.Models;

namespace Playground.Application.Features.ToDoItems.Create.Models
{
    public static class IsCompletedToDoItemInputExtensions
    {
        public static string ToWarning(this IsCompletedToDoItemInput input)
        {
            return $@"{nameof(input.Id)}:{input.Id}|{nameof(input.IsCompleted)}:{input.IsCompleted}|{nameof(input.FormattedErrosList)}:{input.FormattedErrosList()}";
        }

        public static string ToError(this IsCompletedToDoItemInput input)
        {
            return $@"{nameof(input.Id)}:{input.Id}|{nameof(input.IsCompleted)}:{input.IsCompleted}";
        }
    }
}
