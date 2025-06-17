using Playground.Application.Features.ToDoItems.Query.GetById.Models;

namespace Playground.Tests.Controllers
{
    public class GetByIdToDoItemQueryExtensionsTest
    {
        [Fact(DisplayName = "ToWarning DeveGerarStringCorreta")]
        public void ToWarning_DeveGerarStringCorreta()
        {
            var query = new GetByIdToDoItemQuery();
            query.SetId(4);

            var warning = query.ToWarning();

            Assert.Equal("Id:4|FormattedErrosList:()", warning);
        }

        [Fact(DisplayName = "ToInformation DeveGerarStringCorreta")]
        public void ToInformation_DeveGerarStringCorreta()
        {
            var query = new GetByIdToDoItemQuery();
            query.SetId(4);

            var info = query.ToInformation();

            Assert.Equal("Id:4", info);
        }
    }
}
