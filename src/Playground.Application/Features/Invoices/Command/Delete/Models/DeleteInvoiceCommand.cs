using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Playground.Application.Shared.Features.Models;
using System.Text.Json.Serialization;

namespace Playground.Application.Features.Invoices.Command.Delete.Models
{
    [BindNever]
    public class DeleteInvoiceCommand : ValidatableInputBase, IRequest<DeleteInvoiceOutput>
    {
        public DeleteInvoiceCommand(long id)
        {
            Id = id;
        }

        [BindNever]
        [JsonIgnore]
        [JsonPropertyName("id")]
        public long Id { get; }

        public override IEnumerable<string> ErrosList()
        {
            var contract = new Contract<Notification>()
                .Requires()
                .IsGreaterThan(Id, 0, nameof(Id), $"{nameof(Id)} precisa ser maior que zero");

            return GenerateErrorList(contract);
        }
    }
}
