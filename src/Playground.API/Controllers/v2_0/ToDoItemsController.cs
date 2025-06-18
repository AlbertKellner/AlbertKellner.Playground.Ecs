using MediatR;
using Microsoft.AspNetCore.Mvc;
using Playground.Application.Features.ToDoItems.Query.GetById.Models;
using System.Net;

namespace Playground.API.Controllers.v2_0
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("todo")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public class ToDoItemsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ToDoItemController> _logger;

        public ToDoItemsController(
            IMediator mediator,
            ILogger<ToDoItemController> logger)
        {
            _mediator = mediator;
            _logger = logger;
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
            input.SetId(id);

            if (input.IsInvalid())
            {
                _logger.LogWarning($"[Api][ToDoItemController][GetByIdAsync][BadRequest] V2 Teste - input:({input.ToWarning()})");

                return BadRequest(input.ErrosList());
            }

            var output = await _mediator.Send(input, cancellationToken);

            if (output.IsValid())
            {
                _logger.LogWarning($"[Api][ToDoItemController][GetByIdAsync][Ok]");

                return Ok(output);
            }

            _logger.LogWarning($"[Api][ToDoItemController][GetByIdAsync][NoContent]");

            return NoContent();
        }
    }
}
