using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace Playground.Application.Features.ToDoItems.GetById.Models
{
    public class GetByIdToDoItemInput : IRequest<GetByIdToDoItemOutput>
    {
        [BindNever]
        [JsonPropertyName("id")]
        public long Id { get; set; }

        public IEnumerable<string> ErrosList()
        {
            var validationErrors = new List<string>
            {
                Id <= 0 ? $"{nameof(Id)} precisa ser maior que zero" : string.Empty
            };

            validationErrors.RemoveAll(item => item == string.Empty);

            return validationErrors;
        }

        public void SetId(long id)
        {
            Id = id;
        }

        public bool IsInvalid() => ErrosList().Any();

        public string FormattedErrosList() => $"({string.Join("|", ErrosList())})";
    }
}
