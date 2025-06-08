using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Playground.Application.Shared.Features.Models;
using System.Text.Json.Serialization;

namespace Playground.Application.Features.Invoices.Command.Create.Models
{
    public class CreateInvoiceCommand : ValidatableInputBase, IRequest<CreateInvoiceOutput>
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("value")]
        public decimal Value { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        public override IEnumerable<string> ErrosList()
        {
            var contract = new Contract<Notification>()
                .Requires()
                .IsGreaterThan(Value, 0, nameof(Value), $"{nameof(Value)} deve ser maior que zero")
                .IsNotNullOrWhiteSpace(Description, nameof(Description), $"{nameof(Description)} n\u00E3o pode ser vazio ou somente espa\u00E7os em branco");

            return GenerateErrorList(contract);
        }
    }
}
