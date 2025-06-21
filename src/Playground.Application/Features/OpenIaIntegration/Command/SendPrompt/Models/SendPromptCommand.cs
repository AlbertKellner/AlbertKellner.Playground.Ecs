using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Playground.Application.Shared.Features.Models;
using System.Text.Json.Serialization;

namespace Playground.Application.Features.OpenIaIntegration.Command.SendPrompt.Models
{
    public class SendPromptCommand : ValidatableInputBase, IRequest<SendPromptOutput>
    {
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; } = string.Empty;

        [JsonPropertyName("model")]
        public string Model { get; set; } = "gpt-3.5-turbo";

        [JsonPropertyName("temperature")]
        public float Temperature { get; set; } = 1f;

        public override IEnumerable<string> ErrosList()
        {
            var contract = new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Prompt, nameof(Prompt), $"{nameof(Prompt)} deve estar preenchido")
                .IsNotNullOrEmpty(Model, nameof(Model), $"{nameof(Model)} deve estar preenchido")
                .IsBetween(Temperature, 0, 2, nameof(Temperature), $"{nameof(Temperature)} deve estar entre 0 e 2");

            return GenerateErrorList(contract);
        }
    }
}
