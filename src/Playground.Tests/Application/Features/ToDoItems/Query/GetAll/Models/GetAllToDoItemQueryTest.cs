using Playground.Application.Features.ToDoItems.Query.GetAll.Models;

namespace Playground.Tests.Controllers
{
    public class GetAllToDoItemQueryTest
    {
        [Fact(DisplayName = "ErrosList DeveEstarVazia")]
        public void ErrosList_DeveEstarVazia()
        {
            var query = new GetAllToDoItemQuery();

            Assert.Empty(query.ErrosList());
            Assert.False(query.IsInvalid());
            Assert.Equal("()", query.FormattedErrosList());
        }
    }
}
