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
            if (input.IsInvalid())
            {
                _logger.LogWarning($"[Api][ToDoItemController][CreateAsync][BadRequest] input:({input.ToWarning()})");

                return BadRequest(input.ErrosList());
            }

            var output = await _mediator.Send(input, cancellationToken);

            if (output != null && output.IsCreated())
            {
                _logger.LogInformation($"[Api][ToDoItemController][CreateAsync][Created]");

                return CreatedAtRoute(
                    routeName: "GetById",
                    routeValues: new { id = output.Id },
                    value: output);
            }

            _logger.LogError($"[Api][ToDoItemController][CreateAsync][InternalServerError] input:({input.ToError()})");

            return new StatusCodeResult((int)HttpStatusCode.InternalServerError); 
        }

        [HttpGet("{id:long}", Name = "GetById")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(GetByIdToDoItemOutput), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] long id,
            [FromQuery] GetByIdToDoItemQuery input,
            CancellationToken cancellationToken)
        {
            input.SetId(id);

            if (input.IsInvalid())
            {
                _logger.LogWarning($"[Api][ToDoItemController][GetByIdAsync][BadRequest] input:({input.ToWarning()})");

                return BadRequest(input.ErrosList());
            }

            var output = await _mediator.Send(input, cancellationToken);

            if (output == null)
            {
                _logger.LogError($"[Api][ToDoItemController][GetByIdAsync][InternalServerError] input:({input.ToError()})");

                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }

            if (output.IsValid())
            {
                return Ok(output);
            }

            return NoContent();
        }

        [HttpGet()]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(GetAllToDoItemOutput), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAsync(
            CancellationToken cancellationToken)
        {
            var output = await _mediator.Send(new GetAllToDoItemQuery(), cancellationToken);

            if (output == null)
            {
                _logger.LogError($"[Api][ToDoItemController][GetAllAsync][InternalServerError])");

                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }

            if (output.Any())
            {
                return Ok(output);
            }

            return NoContent();
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute] long id,
            [FromBody] UpdateToDoItemCommand input,
            CancellationToken cancellationToken)
        {
            input.SetId(id);

            if (input.IsInvalid())
            {
                _logger.LogWarning($"[Api][ToDoItemController][UpdateAsync][BadRequest] input:({input.ToWarning()})");

                return BadRequest(input.ErrosList());
            }

            var output = await _mediator.Send(input, cancellationToken);

            if (output != null && output.IsUpdated())
            {
                return NoContent();
            }

            _logger.LogError($"[Api][ToDoItemController][UpdateAsync][InternalServerError] input:({input.ToError()})");

            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }

        [HttpPatch("{id:long}/task-name/{taskName}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PatchTaskNameAsync(
            [FromRoute] long id,
            [FromRoute] string taskName,
            CancellationToken cancellationToken)
        {
            var input = new PatchTaskNameToDoItemCommand();
            input.SetId(id);
            input.SetTaskName(taskName);

            if (input.IsInvalid())
            {
                _logger.LogWarning($"[Api][ToDoItemController][PatchTaskNameAsync][BadRequest] input:({input.ToWarning()})");

                return BadRequest(input.ErrosList());
            }

            var output = await _mediator.Send(input, cancellationToken);

            if (output != null && output.IsUpdated())
            {
                return NoContent();
            }

            _logger.LogError($"[Api][ToDoItemController][PatchTaskNameAsync][InternalServerError] input:({input.ToError()})");

            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }

        [HttpPatch("{id:long}/is-completed/{isCompleted}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PatchIsCompletedAsync(
            [FromRoute] long id,
            [FromRoute] bool isCompleted,
            CancellationToken cancellationToken)
        {
            var input = new IsCompletedToDoItemCommand();
            input.SetId(id);
            input.SetIsCompleted(isCompleted);

            if (input.IsInvalid())
            {
                _logger.LogWarning($"[Api][ToDoItemController][PatchIsCompletedAsync][BadRequest] input:({input.ToWarning()})");

                return BadRequest(input.ErrosList());
            }

            var output = await _mediator.Send(input, cancellationToken);

            if (output != null && output.IsUpdated())
            {
                return NoContent();
            }

            _logger.LogError($"[Api][ToDoItemController][PatchIsCompletedAsync][InternalServerError] input:({input.ToError()})");

            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] long id,
            CancellationToken cancellationToken)
        {
            var input = new DeleteToDoItemCommand(id);

            if (input.IsInvalid())
            {
                _logger.LogWarning($"[Api][ToDoItemController][DeleteAsync][BadRequest] input:({input.ToWarning()})");

                return BadRequest(input.ErrosList());
            }

            var output = await _mediator.Send(input, cancellationToken);

            if (output != null && output.IsDeleted)
            {
                return NoContent();
            }

            _logger.LogError($"[Api][ToDoItemController][DeleteAsync][InternalServerError] input:({input.ToError()})");

            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}
