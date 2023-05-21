using System.Text.Json.Serialization;

namespace Playground.Application.Features.Pokemon.GetByName.Models
{
    public class PokemonGetByNameOutput
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        public bool IsValid() =>
            !string.IsNullOrWhiteSpace(Name);
    }
}
