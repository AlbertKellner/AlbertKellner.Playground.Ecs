using System.Text.Json.Serialization;

namespace Playground.Application.Features.ToDoItems.Command.Create.Models
{
    public class CreateToDoItemOutput
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("task")]
        public string Task { get; set; } = string.Empty;

        [JsonPropertyName("is_completed")]
        public bool IsCompleted { get; set; }

        public bool IsCreated() => Id > 0;
    }
}
