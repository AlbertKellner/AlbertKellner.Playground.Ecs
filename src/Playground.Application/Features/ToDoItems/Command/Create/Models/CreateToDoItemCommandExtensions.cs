namespace Playground.Application.Features.ToDoItems.Command.Create.Models
{
    public static class CreateToDoItemCommandExtensions
    {
        public static string ToWarning(this CreateToDoItemCommand input)
        {
            return $@"{nameof(input.Task)}:{input.Task}|{nameof(input.IsCompleted)}:{input.IsCompleted}|{nameof(input.FormattedErrosList)}:{input.FormattedErrosList()}";
        }

        public static string ToInformation(this CreateToDoItemCommand input)
        {
            return $@"{nameof(input.Task)}:{input.Task}|{nameof(input.IsCompleted)}:{input.IsCompleted}";
        }
    }
}
