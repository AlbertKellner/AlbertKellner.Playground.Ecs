using MediatR;
using Microsoft.AspNetCore.Mvc;
using Playground.Application.Features.Pokemon.GetByName.Models;
using Playground.Application.Shared.Domain.ApiDto;
using Playground.Configs;
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
        [ResponseCache(CacheProfileName = ResponseCacheProfile.For1Second)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetByNamePokemonOutput), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByNameExternalAsync(
            [FromRoute] string name,
            [FromQuery] GetByNamePokemonQuery input,
            CancellationToken cancellationToken)
        {
            input.SetName(name);

            if (input.IsInvalid())
            {
                _logger.LogWarning($"[Api][PokemonController][GetByNameExternalAsync][BadRequest] input:({input.ToWarning()})");

                return BadRequest(input.ErrosList());
            }

            var output = await _mediator.Send(input, cancellationToken);

            if (output.IsValid())
            {
                _logger.LogInformation($"[Api][PokemonController][GetByNameExternalAsync][Ok] input:({input.ToInformation()})");

                return Ok(output);
            }

            _logger.LogInformation($"[Api][PokemonController][GetByNameExternalAsync][NoContent] input:({input.ToInformation()})");

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
                _logger.LogInformation($"[Api][PokemonController][GetByNameInternalAsync][Ok] input:({name})");

                return Ok(
                    new PokemonOutApiDto
                    {
                        Name = "pikachu",
                        BaseExperience = 112,
                        LocationAreaEncounters = "Grass"
                    });
            }

            _logger.LogInformation($"[Api][PokemonController][GetByNameInternalAsync][NoContent] input:({name})");

            return NoContent();
        }
    }
}
