using Playground.Application.Features.ToDoItems.Command.Create.Models;

namespace Playground.Tests.Controllers
{
    public class CreateToDoItemCommandTest
    {
        [Fact(DisplayName = "ErrosList QuandoTaskVazia DeveRetornarErro")]
        public void ErrosList_QuandoTaskVazia_DeveRetornarErro()
        {
            var command = new CreateToDoItemCommand { Task = "" };

            var erros = command.ErrosList().ToList();

            Assert.NotEmpty(erros);
            Assert.True(command.IsInvalid());
            Assert.Equal($"({string.Join("|", erros)})", command.FormattedErrosList());
        }
    }
}
