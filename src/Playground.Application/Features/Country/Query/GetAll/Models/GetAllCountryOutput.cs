using System.Text.Json.Serialization;

namespace Playground.Application.Features.Country.Query.GetAll.Models
{
    public class GetAllCountryOutput
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        public bool IsValid() => !string.IsNullOrEmpty(Name);
    }
}
