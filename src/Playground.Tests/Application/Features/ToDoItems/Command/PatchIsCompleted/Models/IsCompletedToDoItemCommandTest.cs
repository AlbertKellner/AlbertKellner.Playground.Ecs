using Playground.Application.Features.ToDoItems.Command.PatchIsCompleted.Models;

namespace Playground.Tests.Controllers
{
    public class IsCompletedToDoItemCommandTest
    {
        [Fact]
        public void Setters_DeveAlterarValores()
        {
            var command = new IsCompletedToDoItemCommand();
            command.SetId(5);
            command.SetIsCompleted(true);

            Assert.Equal(5, command.Id);
            Assert.True(command.IsCompleted);
        }

        [Fact]
        public void ErrosList_QuandoIdInvalido_DeveRetornarErro()
        {
            var command = new IsCompletedToDoItemCommand();
            command.SetId(0);

            var erros = command.ErrosList().ToList();

            Assert.NotEmpty(erros);
            Assert.True(command.IsInvalid());
        }
    }
}
