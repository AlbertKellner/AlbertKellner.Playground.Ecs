using Microsoft.AspNetCore.Mvc;
using Playground.Models;

namespace Playground.Controllers.v2_0
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ToDoItemsController : ControllerBase
    {
        private static List<ToDoItem> _toDoItems = new List<ToDoItem>();

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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
    }
}
