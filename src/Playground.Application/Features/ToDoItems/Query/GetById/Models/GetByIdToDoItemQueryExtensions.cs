namespace Playground.Application.Features.ToDoItems.Query.GetById.Models
{
    public static class GetByIdToDoItemQueryExtensions
    {
        public static string ToWarning(this GetByIdToDoItemQuery input)
        {
            return $@"{nameof(input.Id)}:{input.Id}|{nameof(input.FormattedErrosList)}:{input.FormattedErrosList()}";
        }

        public static string ToInformation(this GetByIdToDoItemQuery input)
        {
            return $@"{nameof(input.Id)}:{input.Id}";
        }
    }
}
