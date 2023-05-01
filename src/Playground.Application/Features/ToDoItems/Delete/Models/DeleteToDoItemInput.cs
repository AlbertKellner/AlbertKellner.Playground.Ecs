using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace Playground.Application.Features.ToDoItems.Delete.Models
{
    [BindNever]
    public class DeleteToDoItemInput : IRequest<DeleteToDoItemOutput>
    {
        public DeleteToDoItemInput(long id)
        {
            Id = id;
        }

        [BindNever]
        [JsonIgnore]
        [JsonPropertyName("id")]
        public long Id { get; }

        public IEnumerable<string> ErrosList()
        {
            var validationErrors = new List<string>
            {
                Id <= 0 ? $"{nameof(Id)} precisa ser maior que zero" : string.Empty,
            };

            validationErrors.RemoveAll(item => item == string.Empty);

            return validationErrors;
        }

        public bool IsInvalid() => ErrosList().Any();

        public string FormattedErrosList() => $"({string.Join("|", ErrosList())})";
    }
}
