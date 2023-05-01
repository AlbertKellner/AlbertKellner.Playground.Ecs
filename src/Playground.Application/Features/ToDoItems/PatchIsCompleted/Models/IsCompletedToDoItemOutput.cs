using System.Text.Json.Serialization;

namespace Playground.Application.Features.ToDoItems.IsCompleted.Models
{
    public class IsCompletedToDoItemOutput
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("is_completed")]
        public bool IsCompleted { get; set; }

        public bool IsUpdated() => Id > 0;
    }
}
