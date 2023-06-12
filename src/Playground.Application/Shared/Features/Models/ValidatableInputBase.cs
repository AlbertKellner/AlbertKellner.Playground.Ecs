using Flunt.Notifications;
using Flunt.Validations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Playground.Application.Shared.Features.Models
{
    [BindNever]
    public abstract class ValidatableInputBase : Notifiable<Notification>
    {
        public abstract IEnumerable<string> ErrosList();

        public bool IsInvalid() => ErrosList().Any();

        public string FormattedErrosList() => $"({string.Join("|", ErrosList())})";

        protected IEnumerable<string> GenerateErrorList(Contract<Notification> contract)
        {
            var errorMessages = new List<string>();

            if (!contract.IsValid)
            {
                errorMessages.AddRange(contract.Notifications.Select(notification => notification.Message));
            }

            return errorMessages;
        }
    }
}
