using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playground.ControllerApi.Models;

namespace Playground.ControllerApi.Controllers.v2_0
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("[controller]/[action]")] //TODO: Resolver versionamento
    //[Authorize()]
    public class TraditionalToDoItemsController : ControllerBase //TODO: Resolver versionamento
    {
        private static List<ToDoItem> _toDoItems = new List<ToDoItem>();

        [HttpPost]
        public IActionResult Create(
            [FromBody] ToDoItem newItem,
            [FromHeader(Name = "token")] string userToken)
        {
            //Implementação da versão 2 do método Create

            int nextId = (_toDoItems.Count > 0 ? _toDoItems.Max(x => x.Id) : 0) + 1;
            newItem.Id = nextId;

            _toDoItems.Add(newItem);

            return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
        }

        [HttpGet]
        public IActionResult GetAll(
            [FromHeader(Name = "token")] string userToken)
            => throw new NotImplementedException("This method only exists in version 1.0");

        [HttpGet("{id}")]
        public IActionResult GetById(
            [FromRoute] int id,
            [FromHeader(Name = "token")] string userToken)
        {
            throw new NotImplementedException("This method only exists in version 1.0");
        }

        [HttpPut("{id}")]
        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] ToDoItem updatedItem,
            [FromHeader(Name = "token")] string userToken)
        {
            throw new NotImplementedException("This method only exists in version 1.0");
        }

        [HttpPatch("{id}/completed")]
        public IActionResult PatchCompleted(
            [FromRoute] int id,
            [FromBody] bool isCompleted,
            [FromHeader(Name = "token")] string userToken)
        {
            throw new NotImplementedException("This method only exists in version 1.0");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(
            [FromRoute] int id,
            [FromHeader(Name = "token")] string userToken)
        {
            throw new NotImplementedException("This method only exists in version 1.0");
        }
    }
}
