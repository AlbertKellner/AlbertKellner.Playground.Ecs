using MediatR;
using System.Text.Json.Serialization;

namespace Playground.Application.Features.ToDoItems.IsCompleted.Models
{
    public class IsCompletedToDoItemInput : IRequest<IsCompletedToDoItemOutput>
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("is_completed")]
        public bool IsCompleted { get; set; }

        public IEnumerable<string> ErrosList()
        {
            var validationErrors = new List<string>
            {
                Id <= 0 ? $"{nameof(Id)} precisa ser maior que zero" : string.Empty
            };

            validationErrors.RemoveAll(item => item == string.Empty);

            return validationErrors;
        }

        public bool IsInvalid() => ErrosList().Any();

        public string FormattedErrosList() => $"({string.Join("|", ErrosList())})";

        public void SetId(long id) => Id = id;

        public void SetIsCompleted(bool isCompleted) => IsCompleted = isCompleted;
    }
}
