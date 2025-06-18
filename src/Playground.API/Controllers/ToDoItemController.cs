using MediatR;
using Microsoft.AspNetCore.Mvc;
using Playground.Application.Features.ToDoItems.Command.Create.Models;
using Playground.Application.Features.ToDoItems.Command.Delete.Models;
using Playground.Application.Features.ToDoItems.Command.PatchIsCompleted.Models;
using Playground.Application.Features.ToDoItems.Command.PatchTaskName.Models;
using Playground.Application.Features.ToDoItems.Command.Update.Models;
using Playground.Application.Features.ToDoItems.Query.GetAll.Models;
using Playground.Application.Features.ToDoItems.Query.GetById.Models;
using System.Net;

namespace Playground.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("todo")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public class ToDoItemController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ToDoItemController> _logger;

        public ToDoItemController(
            IMediator mediator,
            ILogger<ToDoItemController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateToDoItemOutput), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAsync(
            [FromBody] CreateToDoItemCommand input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Api][ToDoItemController][CreateAsync][Start] input:({input.ToInformation()})");

            if (input.IsInvalid())
            {
                _logger.LogWarning($"[Api][ToDoItemController][CreateAsync][BadRequest] input:({input.ToWarning()})");
                return BadRequest(input.ErrosList());
            }

            var output = await _mediator.Send(input, cancellationToken);

            _logger.LogInformation($"[Api][ToDoItemController][CreateAsync][Created] input:({input.ToInformation()})");
            return CreatedAtRoute(
                routeName: "GetById",
                routeValues: new { id = output.Id },
                value: output);
        }

        [HttpGet("{id:long}", Name = "GetById")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetByIdToDoItemOutput), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] long id,
            [FromQuery] GetByIdToDoItemQuery input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Api][ToDoItemController][GetByIdAsync][Start] input:({input.ToInformation()})");

            input.SetId(id);

            if (input.IsInvalid())
            {
                _logger.LogWarning($"[Api][ToDoItemController][GetByIdAsync][BadRequest] input:({input.ToWarning()})");
                return BadRequest(input.ErrosList());
            }

            var output = await _mediator.Send(input, cancellationToken);

            if (output.IsValid())
            {
                _logger.LogInformation($"[Api][ToDoItemController][GetByIdAsync][Ok] input:({input.ToInformation()})");
                return Ok(output);
            }

            _logger.LogInformation($"[Api][ToDoItemController][GetByIdAsync][NoContent] input:({input.ToInformation()})");
            return NoContent();
        }

        [HttpGet()]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(GetAllToDoItemOutput), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAsync(
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Api][ToDoItemController][GetAllAsync][Start]");

            var output = await _mediator.Send(new GetAllToDoItemQuery(), cancellationToken);

            if (output.Any())
            {
                _logger.LogInformation($"[Api][ToDoItemController][GetAllAsync][Ok]");
                return Ok(output);
            }

            _logger.LogInformation($"[Api][ToDoItemController][GetAllAsync][NoContent]");
            return NoContent();
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute] long id,
            [FromBody] UpdateToDoItemCommand input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Api][ToDoItemController][UpdateAsync][Start] input:({input.ToInformation()})");

            input.SetId(id);

            if (input.IsInvalid())
            {
                _logger.LogWarning($"[Api][ToDoItemController][UpdateAsync][BadRequest] input:({input.ToWarning()})");
                return BadRequest(input.ErrosList());
            }

            var output = await _mediator.Send(input, cancellationToken);

            if (output.IsValid())
            {
                _logger.LogInformation($"[Api][ToDoItemController][UpdateAsync][Ok] input:({input.ToInformation()})");
                return Ok();
            }

            _logger.LogInformation($"[Api][ToDoItemController][UpdateAsync][NoContent] input:({input.ToInformation()})");
            return NoContent();
        }

        [HttpPatch("{id:long}/task-name/{taskName}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PatchTaskNameAsync(
        [FromRoute] long id,
        [FromRoute] string taskName,
        [FromRoute] PatchTaskNameToDoItemCommand input,
        CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Api][ToDoItemController][PatchTaskNameAsync][Start] input:({input.ToInformation()})");

            input.SetId(id);
            input.SetTaskName(taskName);

            if (input.IsInvalid())
            {
                _logger.LogWarning($"[Api][ToDoItemController][PatchTaskNameAsync][BadRequest] input:({input.ToWarning()})");
                return BadRequest(input.ErrosList());
            }

            var output = await _mediator.Send(input, cancellationToken);

            if (output.IsValid())
            {
                _logger.LogInformation($"[Api][ToDoItemController][PatchTaskNameAsync][Ok] input:({input.ToInformation()})");
                return Ok();
            }

            _logger.LogInformation($"[Api][ToDoItemController][PatchTaskNameAsync][NoContent] input:({input.ToInformation()})");
            return NoContent();
        }

        [HttpPatch("{id:long}/is-completed/{isCompleted}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PatchIsCompletedAsync(
            [FromRoute] long id,
            [FromRoute] bool isCompleted,
            CancellationToken cancellationToken)
        {
            var input = new IsCompletedToDoItemCommand(); //TODO: Extrair para parametro
            
            _logger.LogInformation($"[Api][ToDoItemController][PatchIsCompletedAsync][Start] input:({input.ToInformation()})");

            input.SetId(id);
            input.SetIsCompleted(isCompleted);

            if (input.IsInvalid())
            {
                _logger.LogWarning($"[Api][ToDoItemController][PatchIsCompletedAsync][BadRequest] input:({input.ToWarning()})");
                return BadRequest(input.ErrosList());
            }

            var output = await _mediator.Send(input, cancellationToken);

            if (output.IsValid())
            {
                _logger.LogInformation($"[Api][ToDoItemController][PatchIsCompletedAsync][Ok] input:({input.ToInformation()})");
                return Ok();
            }

            _logger.LogInformation($"[Api][ToDoItemController][PatchIsCompletedAsync][NoContent] input:({input.ToInformation()})");
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] long id,
            CancellationToken cancellationToken)
        {
            var input = new DeleteToDoItemCommand(id); //TODO: Extrair para parametro

            _logger.LogInformation($"[Api][ToDoItemController][DeleteAsync][Start] input:({input.ToInformation()})");

            if (input.IsInvalid())
            {
                _logger.LogWarning($"[Api][ToDoItemController][DeleteAsync][BadRequest] input:({input.ToWarning()})");
                return BadRequest(input.ErrosList());
            }

            var output = await _mediator.Send(input, cancellationToken);

            if (output.IsValid())
            {
                _logger.LogInformation($"[Api][ToDoItemController][DeleteAsync][Ok] input:({input.ToInformation()})");
                return Ok();
            }

            _logger.LogInformation($"[Api][ToDoItemController][DeleteAsync][NoContent] input:({input.ToInformation()})");
            return NoContent();
        }
    }
}
