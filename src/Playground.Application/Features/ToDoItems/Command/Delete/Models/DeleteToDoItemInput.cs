using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Playground.Application.Shared.Features.Models;
using System.Text.Json.Serialization;

namespace Playground.Application.Features.ToDoItems.Command.Delete.Models
{
    [BindNever]
    public class DeleteToDoItemInput : ValidatableInputBase, IRequest<DeleteToDoItemOutput>
    {
        public DeleteToDoItemInput(long id)
        {
            Id = id;
        }

        [BindNever]
        [JsonIgnore]
        [JsonPropertyName("id")]
        public long Id { get; }
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
