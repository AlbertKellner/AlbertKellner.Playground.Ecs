using Playground.Application.Features.ToDoItems.Command.Delete.Models;

namespace Playground.Tests.Controllers
{
    public class DeleteToDoItemOutputTest
    {
        [Fact]
        public void IsValid_DeveRetornarTrue()
        {
            var output = new DeleteToDoItemOutput();
            Assert.True(output.IsValid());
        }
    }
}
