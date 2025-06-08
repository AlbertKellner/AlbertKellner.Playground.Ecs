using System.Text.Json.Serialization;

namespace Playground.Application.Features.Invoices.Query.GetAll.Models
{
    public class GetAllInvoiceOutput
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("value")]
        public decimal Value { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        public bool IsValid() => Id > 0 && !string.IsNullOrEmpty(Description);
    }
}
