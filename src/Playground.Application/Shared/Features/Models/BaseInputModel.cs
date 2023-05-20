using Flunt.Notifications;

namespace Playground.Application.Shared.Features.Models
{
    public abstract class ValidatableInputBase : Notifiable<Notification>
    {
        public abstract IEnumerable<string> ErrosList();

        public bool IsInvalid() => ErrosList().Any();

        public void ClearErrorMessages() => Clear();

        public string FormattedErrosList() => $"({string.Join("|", ErrosList())})";

        public IEnumerable<string> ValidationMessages() => Notifications.Select(notification => notification.Message);
    }
}
