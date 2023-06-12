namespace Playground.Application.Features.ToDoItems.Command.Delete.Models
{
    public static class DeleteToDoItemInputExtensions
    {
        public static string ToWarning(this DeleteToDoItemInput input)
        {
            return $@"{nameof(input.Id)}:{input.Id}|{nameof(input.FormattedErrosList)}:{input.FormattedErrosList()}";
        }

        public static string ToError(this DeleteToDoItemInput input)
        {
            return $@"{nameof(input.Id)}:{input.Id}";
        }
    }
}
