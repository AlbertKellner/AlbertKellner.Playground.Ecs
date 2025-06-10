using Playground.Application.Features.ToDoItems.Command.PatchIsCompleted.Models;

namespace Playground.Tests.Controllers
{
    public class IsCompletedToDoItemCommandExtensionsTest
    {
        [Fact]
        public void ToWarning_DeveGerarStringCorreta()
        {
            var command = new IsCompletedToDoItemCommand { Id = 1, IsCompleted = true };

            var warning = command.ToWarning();

            Assert.Equal("Id:1|IsCompleted:True|FormattedErrosList:()", warning);
        }

        [Fact]
        public void ToInformation_DeveGerarStringCorreta()
        {
            var command = new IsCompletedToDoItemCommand { Id = 1, IsCompleted = true };

            var info = command.ToInformation();

            Assert.Equal("Id:1|IsCompleted:True", info);
        }
    }
}
