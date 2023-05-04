using MediatR;
using System.Text.Json.Serialization;

namespace Playground.Application.Features.ToDoItems.Create.Models
{
    public class CreateToDoItemInput : IRequest<CreateToDoItemOutput>
    {
        [JsonPropertyName("task")]
        public string Task { get; set; } = string.Empty;

        [JsonPropertyName("is_completed")]
        public bool IsCompleted = false;

        public IEnumerable<string> ErrosList()
        {
            var validationErrors = new List<string>
            {
                string.IsNullOrWhiteSpace(Task) ? $"{nameof(Task)} precisa ser preenchido" : string.Empty
            };

            validationErrors.RemoveAll(item => item == string.Empty);

            return validationErrors;
        }

        public bool IsInvalid() => ErrosList().Any();

        public string FormattedErrosList() => $"({string.Join("|", ErrosList())})";
    }

    public static class CreateToDoItemInputExtensions
    {
        public static string ToWarning(this CreateToDoItemInput input)
        {
            return $@"{nameof(input.Task)}:{input.Task}|{nameof(input.IsCompleted)}:{input.IsCompleted}|{nameof(input.FormattedErrosList)}:{input.FormattedErrosList()}";
        }

        public static string ToError(this CreateToDoItemInput input)
        {
            return $@"{nameof(input.Task)}:{input.Task}|{nameof(input.IsCompleted)}:{input.IsCompleted}";
        }
    }

}
