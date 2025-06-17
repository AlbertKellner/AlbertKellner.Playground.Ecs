using Playground.Application.Features.ToDoItems.Query.GetById.Models;

namespace Playground.Tests.Controllers
{
    public class GetByIdToDoItemOutputTest
    {
        [Fact(DisplayName = "IsValid QuandoDadosValidos DeveRetornarTrue")]
        public void IsValid_QuandoDadosValidos_DeveRetornarTrue()
        {
            var output = new GetByIdToDoItemOutput { Id = 1, Task = "task", IsCompleted = false };
            Assert.True(output.IsValid());
        }
    }
}
