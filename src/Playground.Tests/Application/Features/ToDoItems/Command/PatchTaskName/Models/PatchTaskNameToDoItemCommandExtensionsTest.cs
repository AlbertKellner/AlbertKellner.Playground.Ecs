using Playground.Application.Features.ToDoItems.Command.PatchTaskName.Models;

namespace Playground.Tests.Controllers
{
    public class PatchTaskNameToDoItemCommandExtensionsTest
    {
        [Fact]
        public void ToWarning_DeveGerarStringCorreta()
        {
            var command = new PatchTaskNameToDoItemCommand { Id = 1, TaskName = "name" };

            var warning = command.ToWarning();

            Assert.Equal("Id:1|TaskName:name|FormattedErrosList:()", warning);
        }

        [Fact]
        public void ToInformation_DeveGerarStringCorreta()
        {
            var command = new PatchTaskNameToDoItemCommand { Id = 1, TaskName = "name" };

            var info = command.ToInformation();

            Assert.Equal("Id:1|TaskName:name", info);
        }
    }
}
