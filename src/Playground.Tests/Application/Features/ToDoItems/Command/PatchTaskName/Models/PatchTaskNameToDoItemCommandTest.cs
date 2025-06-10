using Playground.Application.Features.ToDoItems.Command.PatchTaskName.Models;

namespace Playground.Tests.Controllers
{
    public class PatchTaskNameToDoItemCommandTest
    {
        [Fact]
        public void Setters_DeveAlterarValores()
        {
            var command = new PatchTaskNameToDoItemCommand();
            command.SetId(10);
            command.SetTaskName("update");

            Assert.Equal(10, command.Id);
            Assert.Equal("update", command.TaskName);
        }

        [Fact]
        public void ErrosList_QuandoDadosInvalidos_DeveRetornarErro()
        {
            var command = new PatchTaskNameToDoItemCommand();
            command.SetId(0);
            command.SetTaskName("");

            var erros = command.ErrosList().ToList();

            Assert.NotEmpty(erros);
            Assert.True(command.IsInvalid());
        }
    }
}
