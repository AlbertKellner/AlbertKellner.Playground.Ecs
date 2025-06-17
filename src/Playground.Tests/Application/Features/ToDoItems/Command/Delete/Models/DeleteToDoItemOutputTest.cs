using Playground.Application.Features.ToDoItems.Command.Delete.Models;

namespace Playground.Tests.Controllers
{
    public class DeleteToDoItemOutputTest
    {
        [Fact(DisplayName = "IsValid DeveRetornarTrue")]
        public void IsValid_DeveRetornarTrue()
        {
            var output = new DeleteToDoItemOutput();
            Assert.True(output.IsValid());
        }
    }
}
