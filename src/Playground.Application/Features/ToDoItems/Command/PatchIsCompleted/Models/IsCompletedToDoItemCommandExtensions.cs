namespace Playground.Application.Features.ToDoItems.Command.PatchIsCompleted.Models
{
    public static class IsCompletedToDoItemCommandExtensions
    {
        public static string ToWarning(this IsCompletedToDoItemCommand input)
        {
            return $@"{nameof(input.Id)}:{input.Id}|{nameof(input.IsCompleted)}:{input.IsCompleted}|{nameof(input.FormattedErrosList)}:{input.FormattedErrosList()}";
        }

        public static string ToError(this IsCompletedToDoItemCommand input)
        {
            return $@"{nameof(input.Id)}:{input.Id}|{nameof(input.IsCompleted)}:{input.IsCompleted}";
        }
    }
}
