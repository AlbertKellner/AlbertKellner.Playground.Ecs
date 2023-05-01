using System.Text.Json.Serialization;

namespace Playground.Application.Features.ToDoItems.Create.Models
{
    public class CreateToDoItemOutput
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("task")]
        public string Task { get; set; } = string.Empty;

        [JsonPropertyName("is_completed")]
        public bool IsCompleted { get; set; }

        [JsonIgnore]
        public bool IsCreated => Id > 0;
    }
}
