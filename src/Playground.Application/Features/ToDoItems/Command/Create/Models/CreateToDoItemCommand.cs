using MediatR;
using System.Text.Json.Serialization;
using Flunt.Notifications;
using Flunt.Validations;
using Playground.Application.Shared.Features.Models;

namespace Playground.Application.Features.ToDoItems.Command.Create.Models
{
    public class CreateToDoItemCommand : ValidatableInputBase, IRequest<CreateToDoItemOutput>
    {
        [JsonPropertyName("task")]
        public string Task { get; set; } = string.Empty;

        [JsonPropertyName("is_completed")]
        public bool IsCompleted = false;

        public override IEnumerable<string> ErrosList()
        {
            var contract = new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Task, nameof(Task), $"{nameof(Task)} não pode ser vazio ou somente espaços em branco");

            return GenerateErrorList(contract);
        }
    }
}
