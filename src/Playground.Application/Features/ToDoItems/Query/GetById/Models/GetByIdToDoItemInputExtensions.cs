namespace Playground.Application.Features.ToDoItems.Query.GetById.Models
{
    public static class GetByIdToDoItemInputExtensions
    {
        public static string ToWarning(this GetByIdToDoItemInput input)
        {
            return $@"{nameof(input.Id)}:{input.Id}|{nameof(input.FormattedErrosList)}:{input.FormattedErrosList()}";
        }

        public static string ToError(this GetByIdToDoItemInput input)
        {
            return $@"{nameof(input.Id)}:{input.Id}";
        }
    }
}
