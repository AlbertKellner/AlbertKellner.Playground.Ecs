using Playground.Application.Features.ToDoItems.Command.Create.Models;

namespace Playground.Tests.Controllers
{
    public class CreateTodoItemOutputTest
    {
        [Fact]
        public void IsCreated_QuandoIdMaiorQueZero_DeveRetornarTrue()
        {
            var output = new CreateToDoItemOutput { Id = 1 };
            Assert.True(output.IsCreated());
        }
    }
}
