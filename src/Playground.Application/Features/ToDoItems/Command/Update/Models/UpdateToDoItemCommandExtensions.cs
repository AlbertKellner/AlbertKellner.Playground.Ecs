namespace Playground.Application.Features.ToDoItems.Command.Update.Models
{
    public static class UpdateToDoItemCommandExtensions
    {
        public static string ToWarning(this UpdateToDoItemCommand input)
        {
            return $@"{nameof(input.Id)}:{input.Id}|{nameof(input.Task)}:{input.Task}|{nameof(input.IsCompleted)}:{input.IsCompleted}|{nameof(input.FormattedErrosList)}:{input.FormattedErrosList()}";
        }

        public static string ToInformation(this UpdateToDoItemCommand input)
        {
            return $@"{nameof(input.Id)}:{input.Id}|{nameof(input.Task)}:{input.Task}|{nameof(input.IsCompleted)}:{input.IsCompleted}";
        }
    }
}
