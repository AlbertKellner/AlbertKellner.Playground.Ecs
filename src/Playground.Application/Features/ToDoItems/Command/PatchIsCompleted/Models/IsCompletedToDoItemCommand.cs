using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Playground.Application.Shared.Features.Models;
using System.Text.Json.Serialization;

namespace Playground.Application.Features.ToDoItems.Command.PatchIsCompleted.Models
{
    public class IsCompletedToDoItemCommand : ValidatableInputBase, IRequest<IsCompletedToDoItemOutput>
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("is_completed")]
        public bool IsCompleted { get; set; }

        public void SetId(long id) => Id = id;

        public void SetIsCompleted(bool isCompleted) => IsCompleted = isCompleted;

        public override IEnumerable<string> ErrosList()
        {
            ClearErrorMessages();

            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsGreaterThan(Id, (long)0, nameof(Id), $"{nameof(Id)} precisa ser maior que zero")
                );

            return ValidationMessages();
        }
    }
}
