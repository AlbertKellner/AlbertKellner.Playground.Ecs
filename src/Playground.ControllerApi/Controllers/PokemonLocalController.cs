using MediatR;
using Microsoft.AspNetCore.Mvc;
using Playground.Application.Shared.Domain.ApiDto;
using System.Net;

namespace Playground.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("pokemon-local")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public class PokemonLocalController : ControllerBase
    {
        private readonly ILogger<PokemonController> _logger;

        public PokemonLocalController(
            ILogger<PokemonController> logger)
        {
            _logger = logger;
        }

        [HttpGet("internal-name/{name}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(PokemonOutApiDto), (int)HttpStatusCode.OK)]
        public IActionResult GetByNameAsync(
            [FromRoute] string name)
        {
            if (name == "pikachu")
            {
                _logger.LogInformation($"[Api][PokemonLocalController][GetByNameAsync][Ok] input:({name})");

                return Ok(new PokemonOutApiDto { Name = "pikachu" });
            }

            _logger.LogInformation($"[Api][PokemonLocalController][GetByNameAsync][NoContent] input:({name})");

            return NoContent();
        }
    }
}
