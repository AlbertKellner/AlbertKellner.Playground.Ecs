using MediatR;
using Microsoft.AspNetCore.Mvc;
using Playground.Application.Features.Country.Query.GetAll.Models;
using Playground.Application.Features.Country.Query.GetByName.Models;
using System.Net;

namespace Playground.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("Country")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public class CountryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CountryController> _logger;

        public CountryController(
            IMediator mediator,
            ILogger<CountryController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpGet("{id:long}", Name = "GetByName")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetByNameCountryOutput), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByNameAsync(
            [FromRoute] long id,
            [FromQuery] GetByNameCountryQuery input,
            CancellationToken cancellationToken)
        {
            input.SetId(id);

            if (input.IsInvalid())
            {
                _logger.LogWarning($"[Api][CountryController][GetByIdAsync][BadRequest] input:({input.ToWarning()})");

                return BadRequest(input.ErrosList());
            }

            var output = await _mediator.Send(input, cancellationToken);

            if (output.IsValid())
            {
                _logger.LogInformation($"[Api][CountryController][GetByIdAsync][Ok] input:({input.ToInformation()})");

                return Ok(output);
            }

            _logger.LogWarning($"[Api][CountryController][GetByIdAsync][NoContent] input:({input.ToInformation()})");

            return NoContent();
        }

        [HttpGet()]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(GetAllCountryOutput), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAsync(
            CancellationToken cancellationToken)
        {
            var output = await _mediator.Send(new GetAllCountryQuery(), cancellationToken);

            if (output.Any())
            {
                _logger.LogInformation($"[Api][CountryController][GetAllAsync][Ok]");

                return Ok(output);
            }

            _logger.LogWarning($"[Api][CountryController][GetAllAsync][NoContent]");

            return NoContent();
        }
    }
}
