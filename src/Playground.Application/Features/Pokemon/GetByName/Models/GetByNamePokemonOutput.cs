using System.Text.Json.Serialization;

namespace Playground.Application.Features.Pokemon.GetByName.Models
{
    public class GetByNamePokemonOutput
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("base-experience")]
        public int BaseExperience { get; set; } = 0;
        [JsonPropertyName("location-area-encounters")]
        public string LocationAreaEncounters { get; set; } = string.Empty;

        public bool IsValid() =>
            !string.IsNullOrWhiteSpace(Name);
    }
}
