using Playground.Application.Features.ToDoItems.Command.Update.Models;

namespace Playground.Tests.Controllers
{
    public class UpdateToDoItemOutputTest
    {
        [Fact]
        public void IsValid_QuandoDadosValidos_DeveRetornarTrue()
        {
            var output = new UpdateToDoItemOutput { Id = 1, Task = "task", IsCompleted = true };
            Assert.True(output.IsValid());
        }
    }
}
