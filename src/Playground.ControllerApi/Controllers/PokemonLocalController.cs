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
        [HttpGet("{name}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(PokemonOutApiDto), (int)HttpStatusCode.OK)]
        public IActionResult GetByNameAsync(
            [FromRoute] string name)
        {
            if (name == "pikachu")
            {
                return Ok(new PokemonOutApiDto { Name = "pikachu" });
            }
            return NoContent();
        }
    }
}
