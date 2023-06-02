using System.Text.Json.Serialization;

namespace Playground.Application.Shared.Domain.ApiDto
{
    public class PokemonOutApiDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("base-experience")]
        public int BaseExperience { get; set; } = 0;
        [JsonPropertyName("location-area-encounters")]
        public string LocationAreaEncounters { get; set; } = string.Empty;
    }
}
