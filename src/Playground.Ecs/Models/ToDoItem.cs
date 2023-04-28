using System.Text.Json.Serialization;

namespace Playground.MinimalApi.Models
{
    public class ToDoItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("task")]
        public string Task { get; set; }

        [JsonPropertyName("is_completed")]
        public bool IsCompleted { get; set; }
    }
}
