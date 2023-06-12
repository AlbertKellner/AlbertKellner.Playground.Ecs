using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Playground.Application.Shared.Features.Models;

namespace Playground.Application.Features.ToDoItems.Command.PatchTaskName.Models
{
    public class PatchTaskNameToDoItemInput : ValidatableInputBase, IRequest<PatchTaskNameToDoItemOutput>
    {
        public long Id { get; private set; }

        public string Task { get; set; } = string.Empty;

        public void SetId(long id) => Id = id;

        public void SetTaskName(string taskName) => Task = taskName;

        public override IEnumerable<string> ErrosList()
        {
            ClearErrorMessages();

            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Task, nameof(Task), $"{nameof(Task)} não pode ser vazio ou somente espaços em branco")
                .IsGreaterThan(Id, (long)0, nameof(Id), $"{nameof(Id)} precisa ser maior que zero")
                );

            return ValidationMessages();
        }
    }
}
