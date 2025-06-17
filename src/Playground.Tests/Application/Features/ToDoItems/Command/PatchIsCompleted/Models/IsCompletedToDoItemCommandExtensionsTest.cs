using Playground.Application.Features.ToDoItems.Command.PatchIsCompleted.Models;

namespace Playground.Tests.Controllers
{
    public class IsCompletedToDoItemCommandExtensionsTest
    {
        [Fact(DisplayName = "ToWarning DeveGerarStringCorreta")]
        public void ToWarning_DeveGerarStringCorreta()
        {
            var command = new IsCompletedToDoItemCommand { Id = 1, IsCompleted = true };

            var warning = command.ToWarning();

            Assert.Equal("Id:1|IsCompleted:True|FormattedErrosList:()", warning);
        }

        [Fact(DisplayName = "ToInformation DeveGerarStringCorreta")]
        public void ToInformation_DeveGerarStringCorreta()
        {
            var command = new IsCompletedToDoItemCommand { Id = 1, IsCompleted = true };

            var info = command.ToInformation();

            Assert.Equal("Id:1|IsCompleted:True", info);
        }
    }
}
