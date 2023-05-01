using MediatR;
using Microsoft.AspNetCore.Mvc;
using Playground.Application.Features.ToDoItems.Create.Models;
using Playground.Models;

namespace Playground.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class ToDoItemsController : ControllerBase
    {
        private static List<ToDoItem> _toDoItems = new List<ToDoItem>();

        private readonly IMediator _mediator;

        public ToDoItemsController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateInput input,
            [FromHeader(Name = "token")] string userToken,
            CancellationToken cancellationToken)
        {
            var success = await _mediator.Send(input, cancellationToken);

            //int nextId = (_toDoItems.Count > 0 ? _toDoItems.Max(x => x.Id) : 0) + 1;
            //newItem.Id = nextId;

            //_toDoItems.Add(newItem);

            //return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
            return Ok("");
        }

        [Obsolete]
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
    }
}
