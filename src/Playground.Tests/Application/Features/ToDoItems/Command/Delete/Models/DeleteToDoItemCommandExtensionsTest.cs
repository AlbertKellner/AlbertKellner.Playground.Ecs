using Playground.Application.Features.ToDoItems.Command.Delete.Models;

namespace Playground.Tests.Controllers
{
    public class DeleteToDoItemCommandExtensionsTest
    {
        [Fact]
        public void ToWarning_DeveGerarStringCorreta()
        {
            var command = new DeleteToDoItemCommand(1);

            var warning = command.ToWarning();

            Assert.Equal("Id:1|FormattedErrosList:()", warning);
        }

        [Fact]
        public void ToInformation_DeveGerarStringCorreta()
        {
            var command = new DeleteToDoItemCommand(1);

            var info = command.ToInformation();

            Assert.Equal("Id:1", info);
        }
    }
}
