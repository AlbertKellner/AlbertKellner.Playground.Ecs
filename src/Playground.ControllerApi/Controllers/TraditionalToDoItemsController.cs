using Microsoft.AspNetCore.Mvc;
using Playground.ControllerApi.Models;

namespace Playground.ControllerApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]/[action]")]
    public class TraditionalToDoItemsController : ControllerBase
    {
        private static List<ToDoItem> _toDoItems = new List<ToDoItem>();

        [HttpPost]
        [Obsolete]
        public IActionResult Create(
            [FromBody]ToDoItem newItem,
            [FromHeader(Name = "token")] string userToken)
        {
            int nextId = (_toDoItems.Count > 0 ? _toDoItems.Max(x => x.Id) : 0) + 1;
            newItem.Id = nextId;

            _toDoItems.Add(newItem);

            return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
        }

        [HttpGet]
        public IActionResult GetAll(
            [FromHeader(Name = "token")] string userToken) 
            => Ok(_toDoItems);

        [HttpGet("{id}")]
        public IActionResult GetById(
            [FromRoute] int id,
            [FromHeader(Name = "token")] string userToken)
        {
            var item = _toDoItems.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(
            [FromRoute] int id, 
            [FromBody]ToDoItem updatedItem,
            [FromHeader(Name = "token")] string userToken)
        {
            var index = _toDoItems.FindIndex(x => x.Id == id);

            if (index == -1)
            {
                return NotFound();
            }

            updatedItem.Id = id;

            _toDoItems[index] = updatedItem;

            return Ok();
        }

        [HttpPatch("{id}/completed")]
        public IActionResult PatchCompleted(
            [FromRoute] int id, 
            [FromBody] bool isCompleted,
            [FromHeader(Name = "token")] string userToken)
        {
            var item = _toDoItems.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            item.IsCompleted = isCompleted;

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(
            [FromRoute] int id,
            [FromHeader(Name = "token")] string userToken)
        {
            var item = _toDoItems.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            _toDoItems.Remove(item);

            return Ok();
        }
    }
}
