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
        [JsonPropertyName("name")]
        public string Name { get; internal set; } = string.Empty;

        public void SetName(string name) => Name = name;

        public override IEnumerable<string> ErrosList()
        {
            var contract = new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Name, nameof(Name), $"{nameof(Name)} não pode ser vazio ou somente espaços em branco");

            return GenerateErrorList(contract);
        }
    }
}
