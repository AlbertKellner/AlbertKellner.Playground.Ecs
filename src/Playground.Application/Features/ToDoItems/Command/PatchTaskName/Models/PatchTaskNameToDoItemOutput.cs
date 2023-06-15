using System.Text.Json.Serialization;

namespace Playground.Application.Features.ToDoItems.Command.PatchTaskName.Models
{
    public class PatchTaskNameToDoItemOutput
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("task")]
        public string Task { get; set; } = string.Empty;

        [JsonPropertyName("is_completed")]
        public bool IsCompleted { get; set; }

        public bool IsValid() =>
            Id > 0
            && !string.IsNullOrEmpty(Task);
    }
}
