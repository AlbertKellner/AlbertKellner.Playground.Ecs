using Playground.Application.Features.ToDoItems.Command.PatchTaskName.Models;

namespace Playground.Tests.Controllers
{
    public class PatchTaskNameToDoItemOutputTest
    {
        [Fact(DisplayName = "IsValid QuandoDadosValidos DeveRetornarTrue")]
        public void IsValid_QuandoDadosValidos_DeveRetornarTrue()
        {
            var output = new PatchTaskNameToDoItemOutput { Id = 1, Task = "task", IsCompleted = false };
            Assert.True(output.IsValid());
        }
    }
}
