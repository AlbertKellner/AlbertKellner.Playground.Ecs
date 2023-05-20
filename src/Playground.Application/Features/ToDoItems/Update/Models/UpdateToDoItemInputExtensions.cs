namespace Playground.Application.Features.ToDoItems.Update.Models
{
    public static class UpdateToDoItemInputExtensions
    {
        public static string ToWarning(this UpdateToDoItemInput input)
        {
            return $@"{nameof(input.Id)}:{input.Id}|{nameof(input.Task)}:{input.Task}|{nameof(input.IsCompleted)}:{input.IsCompleted}|{nameof(input.FormattedErrosList)}:{input.FormattedErrosList()}";
        }

        public static string ToError(this UpdateToDoItemInput input)
        {
            return $@"{nameof(input.Id)}:{input.Id}|{nameof(input.Task)}:{input.Task}|{nameof(input.IsCompleted)}:{input.IsCompleted}";
        }
    }
}
