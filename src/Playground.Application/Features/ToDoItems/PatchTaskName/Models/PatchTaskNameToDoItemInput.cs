using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace Playground.Application.Features.ToDoItems.PatchTaskName.Models
{
    public class PatchTaskNameToDoItemInput : IRequest<PatchTaskNameToDoItemOutput>
    {
        public long Id { get; private set; }

        public string Task { get; set; } = string.Empty;

        public IEnumerable<string> ErrosList()
        {
            var validationErrors = new List<string>
            {
                Id <= 0 ? $"{nameof(Id)} precisa ser maior que zero" : string.Empty,
                string.IsNullOrWhiteSpace(Task) ? $"{nameof(Task)} precisa ser preenchido" : string.Empty
            };

            validationErrors.RemoveAll(item => item == string.Empty);

            return validationErrors;
        }

        public bool IsInvalid() => ErrosList().Any();

        public string FormattedErrosList() => $"({string.Join("|", ErrosList())})";

        public void SetId(long id) => Id = id;

        public void SetTaskName(string taskName) => Task = taskName;
    }
}
