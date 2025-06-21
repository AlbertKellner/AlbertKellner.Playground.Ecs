using MediatR;
using Microsoft.AspNetCore.Mvc;
using Playground.Application.Features.OpenIaIntegration.Command.SendPrompt.Models;
using System.Net;

namespace Playground.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("openia")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public class OpenIaIntegrationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OpenIaIntegrationController> _logger;

        public OpenIaIntegrationController(IMediator mediator, ILogger<OpenIaIntegrationController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("prompt")]
        [ProducesResponseType(typeof(SendPromptOutput), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SendPromptAsync([FromBody] SendPromptCommand input, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Api][OpenIaIntegrationController][SendPromptAsync][Start] input:({input.ToInformation()})");

            if (input.IsInvalid())
            {
                _logger.LogWarning($"[Api][OpenIaIntegrationController][SendPromptAsync][BadRequest] input:({input.ToWarning()})");
                return BadRequest(input.ErrosList());
            }

            var output = await _mediator.Send(input, cancellationToken);

            _logger.LogInformation($"[Api][OpenIaIntegrationController][SendPromptAsync][Ok] input:({input.ToInformation()})");
            return Ok(output);
        }
    }
}
