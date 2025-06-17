using Playground.Application.Features.ToDoItems.Command.Update.Models;

namespace Playground.Tests.Controllers
{
    public class UpdateToDoItemCommandTest
    {
        [Fact(DisplayName = "Setters DeveAlterarValores")]
        public void Setters_DeveAlterarValores()
        {
            var command = new UpdateToDoItemCommand();
            command.SetId(8);
            command.Task = "task";
            command.IsCompleted = true;

            Assert.Equal(8, command.Id);
            Assert.Equal("task", command.Task);
            Assert.True(command.IsCompleted);
        }

        [Fact(DisplayName = "ErrosList QuandoDadosInvalidos DeveRetornarErro")]
        public void ErrosList_QuandoDadosInvalidos_DeveRetornarErro()
        {
            var command = new UpdateToDoItemCommand();
            command.SetId(0);
            command.Task = "";

            var erros = command.ErrosList().ToList();

            Assert.NotEmpty(erros);
            Assert.True(command.IsInvalid());
        }
    }
}
