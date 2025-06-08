using System.Text.Json.Serialization;

namespace Playground.Application.Features.Country.Query.GetByName.Models
{
    public class GetByNameCountryOutput
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        public bool IsValid() =>
            !string.IsNullOrEmpty(Name);
    }
}
