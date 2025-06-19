namespace Playground.Application.Features.ToDoItems.Query.GetById.Models
{
    public static class GetByIdToDoItemOldQueryExtensions
    {
        public static string ToWarning(this GetByIdToDoItemOldQuery input)
        {
            return $@"{nameof(input.Id)}:{input.Id}|{nameof(input.FormattedErrosList)}:{input.FormattedErrosList()}";
        }

        public static string ToInformation(this GetByIdToDoItemOldQuery input)
        {
            return $@"{nameof(input.Id)}:{input.Id}";
        }
    }
}
