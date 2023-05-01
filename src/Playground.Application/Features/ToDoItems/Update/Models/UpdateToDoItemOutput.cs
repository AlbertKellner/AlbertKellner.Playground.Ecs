using System.Text.Json.Serialization;

namespace Playground.Application.Features.ToDoItems.Update.Models
{
    public class UpdateToDoItemOutput
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("task")]
        public string Task { get; set; } = string.Empty;

        [JsonPropertyName("is_completed")]
        public bool IsCompleted { get; set; }

        public bool IsUpdated() => 
            Id > 0
            && !string.IsNullOrEmpty(Task);
    }
}
