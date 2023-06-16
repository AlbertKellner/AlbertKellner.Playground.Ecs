namespace Playground.Application.Features.ToDoItems.Command.PatchTaskName.Models
{
    public static class PatchTaskNameToDoItemCommandExtensions
    {
        public static string ToWarning(this PatchTaskNameToDoItemCommand input)
        {
            return $@"{nameof(input.Id)}:{input.Id}|{nameof(input.TaskName)}:{input.TaskName}|{nameof(input.FormattedErrosList)}:{input.FormattedErrosList()}";
        }

        public static string ToInformation(this PatchTaskNameToDoItemCommand input)
        {
            return $@"{nameof(input.Id)}:{input.Id}|{nameof(input.TaskName)}:{input.TaskName}";
        }
    }
}
