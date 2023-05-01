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

        public bool IsInvalid => ErrosList().Any();
    }
}
