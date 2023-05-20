namespace Playground.Application.Features.ToDoItems.Create.Models
{
    public static class CreateToDoItemInputExtensions
    {
        public static string ToWarning(this CreateToDoItemInput input)
        {
            return $@"{nameof(input.Task)}:{input.Task}|{nameof(input.IsCompleted)}:{input.IsCompleted}|{nameof(input.FormattedErrosList)}:{input.FormattedErrosList()}";
        }

        public static string ToError(this CreateToDoItemInput input)
        {
            return $@"{nameof(input.Task)}:{input.Task}|{nameof(input.IsCompleted)}:{input.IsCompleted}";
        }
    }
}
