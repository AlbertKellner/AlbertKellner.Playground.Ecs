using Playground.Application.Features.ToDoItems.Query.GetById.Models;

namespace Playground.Tests.Controllers
{
    public class GetByIdToDoItemQueryTest
    {
        [Fact(DisplayName = "SetId DeveAlterarId")]
        public void SetId_DeveAlterarId()
        {
            var query = new GetByIdToDoItemQuery();
            query.SetId(3);

            Assert.Equal(3, query.Id);
        }

        [Fact(DisplayName = "ErrosList QuandoIdInvalido DeveRetornarErro")]
        public void ErrosList_QuandoIdInvalido_DeveRetornarErro()
        {
            var query = new GetByIdToDoItemQuery();
            query.SetId(0);

            var erros = query.ErrosList().ToList();

            Assert.NotEmpty(erros);
            Assert.True(query.IsInvalid());
        }
    }
}
