using Playground.Application.Features.ToDoItems.Command.Create.Models;

namespace Playground.Tests.Controllers
{
    public class CreateToDoItemCommandExtensionsTest
    {
        [Fact]
        public void ToWarning_DeveGerarStringCorreta()
        {
            var command = new CreateToDoItemCommand { Task = "task", IsCompleted = true };

            var warning = command.ToWarning();

            Assert.Equal("Task:task|IsCompleted:True|FormattedErrosList:()", warning);
        }

        [Fact]
        public void ToInformation_DeveGerarStringCorreta()
        {
            var command = new CreateToDoItemCommand { Task = "task", IsCompleted = true };

            var info = command.ToInformation();

            Assert.Equal("Task:task|IsCompleted:True", info);
        }
    }
}
