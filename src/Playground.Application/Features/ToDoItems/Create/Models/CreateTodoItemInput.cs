using MediatR;
using System.Text.Json.Serialization;
using Flunt.Notifications;
using Flunt.Validations;
using Playground.Application.Shared.Features.Models;

namespace Playground.Application.Features.ToDoItems.Create.Models
{
    public class CreateToDoItemInput : ValidatableInputBase, IRequest<CreateToDoItemOutput>
    {
        [JsonPropertyName("task")]
        public string Task { get; set; } = string.Empty;

        [JsonPropertyName("is_completed")]
        public bool IsCompleted = false;

        public override IEnumerable<string> ErrosList()
        {
            ClearErrorMessages();
            
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Task, nameof(Task), $"{nameof(Task)} should not be empty or whitespace")
                );

            return ValidationMessages();
        }
    }
}
