using Playground.Application.Features.ToDoItems.Command.Update.Models;

namespace Playground.Tests.Controllers
{
    public class UpdateToDoItemCommandExtensionsTest
    {
        [Fact(DisplayName = "ToWarning DeveGerarStringCorreta")]
        public void ToWarning_DeveGerarStringCorreta()
        {
            var command = new UpdateToDoItemCommand { Task = "a", IsCompleted = true };
            command.SetId(1);

            var warning = command.ToWarning();

            Assert.Equal("Id:1|Task:a|IsCompleted:True|FormattedErrosList:()", warning);
        }

        [Fact(DisplayName = "ToInformation DeveGerarStringCorreta")]
        public void ToInformation_DeveGerarStringCorreta()
        {
            var command = new UpdateToDoItemCommand { Task = "a", IsCompleted = true };
            command.SetId(1);

            var info = command.ToInformation();

            Assert.Equal("Id:1|Task:a|IsCompleted:True", info);
        }
    }
}
