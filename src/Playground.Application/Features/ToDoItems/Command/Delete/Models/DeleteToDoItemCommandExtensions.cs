namespace Playground.Application.Features.ToDoItems.Command.Delete.Models
{
    public static class DeleteToDoItemCommandExtensions
    {
        public static string ToWarning(this DeleteToDoItemCommand input)
        {
            return $@"{nameof(input.Id)}:{input.Id}|{nameof(input.FormattedErrosList)}:{input.FormattedErrosList()}";
        }

        public static string ToInformation(this DeleteToDoItemCommand input)
        {
            return $@"{nameof(input.Id)}:{input.Id}";
        }
    }
}
