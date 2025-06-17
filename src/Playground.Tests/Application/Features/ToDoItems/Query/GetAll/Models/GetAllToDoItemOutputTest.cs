using Playground.Application.Features.ToDoItems.Query.GetAll.Models;

namespace Playground.Tests.Controllers
{
    public class GetAllToDoItemOutputTest
    {
        [Fact(DisplayName = "IsValid QuandoDadosValidos DeveRetornarTrue")]
        public void IsValid_QuandoDadosValidos_DeveRetornarTrue()
        {
            var output = new GetAllToDoItemOutput { Id = 1, Task = "task", IsCompleted = false };
            Assert.True(output.IsValid());
        }
    }
}
