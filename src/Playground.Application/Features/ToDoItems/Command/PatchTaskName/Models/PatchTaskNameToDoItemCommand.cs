using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Playground.Application.Shared.Features.Models;

namespace Playground.Application.Features.ToDoItems.Command.PatchTaskName.Models
{
    public class PatchTaskNameToDoItemCommand : ValidatableInputBase, IRequest<PatchTaskNameToDoItemOutput>
    {
        public long Id { get; set; }

        public string TaskName { get; set; } = string.Empty;

        public void SetId(long id) => Id = id;

        public void SetTaskName(string taskName) => TaskName = taskName;

        public override IEnumerable<string> ErrosList()
        {
            var contract = new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(TaskName, nameof(TaskName), $"{nameof(TaskName)} não pode ser vazio ou somente espaços em branco")
                .IsGreaterThan(Id, (long)0, nameof(Id), $"{nameof(Id)} precisa ser maior que zero");

            return GenerateErrorList(contract);
        }
    }
}
