using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Playground.Application.Shared.Features.Models;
using System.Text.Json.Serialization;

namespace Playground.Application.Features.ToDoItems.Command.Update.Models
{
    public class UpdateToDoItemCommand : ValidatableInputBase, IRequest<UpdateToDoItemOutput>
    {
        [BindNever]
        [JsonIgnore]
        [JsonPropertyName("id")]
        public long Id { get; private set; }

        [JsonPropertyName("task")]
        public string Task { get; set; } = string.Empty;

        [JsonPropertyName("is_completed")]
        public bool IsCompleted { get; set; }

        public void SetId(long id) => Id = id;

        public override IEnumerable<string> ErrosList()
        {
            var contract = new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Task, nameof(Task), $"{nameof(Task)} não pode ser vazio ou somente espaços em branco")
                .IsGreaterThan(Id, (long)0, nameof(Id), $"{nameof(Id)} precisa ser maior que zero");

            return GenerateErrorList(contract);
        }
    }
}
