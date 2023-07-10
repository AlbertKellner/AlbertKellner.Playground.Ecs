using System.Text.Json.Serialization;

namespace Playground.Application.Features.Country.Query.GetByName.Models
{
    public class GetByNameCountryOutput
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
