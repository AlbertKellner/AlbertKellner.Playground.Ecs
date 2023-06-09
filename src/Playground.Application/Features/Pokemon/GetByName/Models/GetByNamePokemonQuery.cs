﻿using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Playground.Application.Shared.Features.Models;
using System.Diagnostics.Contracts;
using System.Text.Json.Serialization;

namespace Playground.Application.Features.Pokemon.GetByName.Models
{
    public class GetByNamePokemonQuery : ValidatableInputBase, IRequest<GetByNamePokemonOutput>
    {
        [BindNever]
        [JsonPropertyName("name")]
        public string Name { get; internal set; } = string.Empty;

        public void SetName(string name) => Name = name;

        public override IEnumerable<string> ErrosList()
        {
            var contract = new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Name, nameof(Name), $"{nameof(Name)} deve estar preenchido");

            return GenerateErrorList(contract);
        }
    }
}
