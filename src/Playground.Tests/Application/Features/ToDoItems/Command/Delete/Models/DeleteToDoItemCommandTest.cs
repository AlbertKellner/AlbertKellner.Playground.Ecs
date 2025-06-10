using Playground.Application.Features.ToDoItems.Command.Delete.Models;

namespace Playground.Tests.Controllers
{
    public class DeleteToDoItemCommandTest
    {
        [Fact]
        public void ErrosList_QuandoIdInvalido_DeveRetornarErro()
        {
            var command = new DeleteToDoItemCommand(0);

            var erros = command.ErrosList().ToList();

            Assert.NotEmpty(erros);
            Assert.True(command.IsInvalid());
            Assert.Equal($"({string.Join("|", erros)})", command.FormattedErrosList());
        }
    }
}
