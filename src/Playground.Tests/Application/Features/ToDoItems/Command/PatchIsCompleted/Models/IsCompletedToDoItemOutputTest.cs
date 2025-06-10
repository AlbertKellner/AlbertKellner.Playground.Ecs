using Playground.Application.Features.ToDoItems.Command.PatchIsCompleted.Models;

namespace Playground.Tests.Controllers
{
    public class IsCompletedToDoItemOutputTest
    {
        [Fact]
        public void IsValid_QuandoIdValido_DeveRetornarTrue()
        {
            var output = new IsCompletedToDoItemOutput { Id = 2, IsCompleted = true };
            Assert.True(output.IsValid());
        }
    }
}
