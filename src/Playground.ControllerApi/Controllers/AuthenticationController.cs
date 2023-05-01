using Microsoft.AspNetCore.Mvc;
using Playground.Models;

namespace Playground.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private static List<ToDoItem> _toDoItems = new List<ToDoItem>();

        //[HttpGet("{id}")]
        //public IActionResult GetById(
        //    [FromRoute] int id,
        //    [FromHeader(Name = "token")] string userToken)
        //{
        //    var item = _toDoItems.FirstOrDefault(x => x.Id == id);

        //    if (item == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(item);
        //}
    }
}
