using MediatR;
using Microsoft.AspNetCore.Mvc;
using Playground.Application.Features.Pokemon.GetByName.Models;
using Playground.Application.Features.ToDoItems.Create.Models;
using Playground.Application.Features.ToDoItems.Delete.Models;
using Playground.Application.Features.ToDoItems.GetAll.Models;
using Playground.Application.Features.ToDoItems.GetById.Models;
using Playground.Application.Features.ToDoItems.IsCompleted.Models;
using Playground.Application.Features.ToDoItems.PatchTaskName.Models;
using Playground.Application.Features.ToDoItems.Update.Models;
using Playground.Application.Shared.Domain.ApiDto;
using System.Net;

namespace Playground.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("pokemon")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public class PokemonController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PokemonController> _logger;

        public PokemonController(
            IMediator mediator,
            ILogger<PokemonController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("external-name/{name}")]
        [ResponseCache(CacheProfileName = "ResponseCache:1Second")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(GetByNamePokemonOutput), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByNameExternalAsync(
            [FromRoute] string name,
            [FromQuery] GetByNamePokemonInput input,
            CancellationToken cancellationToken)
        {
            input.SetName(name);

            if (input.IsInvalid())
            {
                _logger.LogWarning($"[Api][PokemonController][GetByNameExternalAsync][BadRequest] input:({input.ToWarning()})");

                return BadRequest(input.ErrosList());
            }

            var output = await _mediator.Send(input, cancellationToken);

            if (output == null)
            {
                _logger.LogError($"[Api][PokemonController][GetByNameExternalAsync][InternalServerError] input:({input.ToError()})");

                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }

            if (output.IsValid())
            {
                _logger.LogInformation($"[Api][PokemonController][GetByNameExternalAsync][Ok] input:({input.ToInformation()})");

                return Ok(output);
            }

            return NoContent();
        }

        [HttpGet("internal-name/{name}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(PokemonOutApiDto), (int)HttpStatusCode.OK)]
        public IActionResult GetByNameInternalAsync(
            [FromRoute] string name)
        {
            if (name == "pikachu")
            {
                _logger.LogInformation($"[Api][PokemonLocalController][GetByNameInternalAsync][Ok] input:({name})");

                return Ok(new PokemonOutApiDto { Name = "pikachu" });
            }

            _logger.LogInformation($"[Api][PokemonLocalController][GetByNameInternalAsync][NoContent] input:({name})");

            return NoContent();
        }
    }
}
