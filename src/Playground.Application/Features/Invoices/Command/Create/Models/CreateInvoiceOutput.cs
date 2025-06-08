using System.Text.Json.Serialization;

namespace Playground.Application.Features.Invoices.Command.Create.Models
{
    public class CreateInvoiceOutput
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("value")]
        public decimal Value { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        public bool IsCreated() => Id > 0;
    }
}
