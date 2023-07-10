using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Playground.Application.Shared.Features.Models;
using System.Text.Json.Serialization;

namespace Playground.Application.Features.Country.Query.GetByName.Models
{
    public class GetByNameCountryQuery : ValidatableInputBase, IRequest<GetByNameCountryOutput>
    {
        [BindNever]
        [JsonPropertyName("id")]
        public long Id { get; internal set; }

        public void SetId(long id) => Id = id;

        public override IEnumerable<string> ErrosList()
        {
            var contract = new Contract<Notification>()
                .Requires()
                .IsGreaterThan(Id, (long)0, nameof(Id), $"{nameof(Id)} precisa ser maior que zero");

            return GenerateErrorList(contract);
        }
    }
}
