using MediatR;
using Microsoft.AspNetCore.Mvc;
using Playground.Application.Features.Invoices.Command.Create.Models;
using Playground.Application.Features.Invoices.Command.Delete.Models;
using Playground.Application.Features.Invoices.Command.Update.Models;
using Playground.Application.Features.Invoices.Query.GetAll.Models;
using Playground.Application.Features.Invoices.Query.GetById.Models;
using System.Net;

namespace Playground.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("invoice")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public class InvoiceController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<InvoiceController> _logger;

        public InvoiceController(
            IMediator mediator,
            ILogger<InvoiceController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateInvoiceOutput), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAsync(
            [FromBody] CreateInvoiceCommand input,
            CancellationToken cancellationToken)
        {
            if (input.IsInvalid())
            {
                return BadRequest(input.ErrosList());
            }

            var output = await _mediator.Send(input, cancellationToken);

            return CreatedAtRoute(
                routeName: "GetInvoiceById",
                routeValues: new { id = output.Id },
                value: output);
        }

        [HttpGet("{id:long}", Name = "GetInvoiceById")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetByIdInvoiceOutput), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] long id,
            [FromQuery] GetByIdInvoiceQuery input,
            CancellationToken cancellationToken)
        {
            input.SetId(id);

            if (input.IsInvalid())
            {
                return BadRequest(input.ErrosList());
            }

            var output = await _mediator.Send(input, cancellationToken);

            if (output.IsValid())
            {
                return Ok(output);
            }

            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(GetAllInvoiceOutput), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAsync(
            CancellationToken cancellationToken)
        {
            var output = await _mediator.Send(new GetAllInvoiceQuery(), cancellationToken);

            if (output.Any())
            {
                return Ok(output);
            }

            return NoContent();
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute] long id,
            [FromBody] UpdateInvoiceCommand input,
            CancellationToken cancellationToken)
        {
            input.SetId(id);

            if (input.IsInvalid())
            {
                return BadRequest(input.ErrosList());
            }

            var output = await _mediator.Send(input, cancellationToken);

            if (output.IsValid())
            {
                return Ok();
            }

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
            var input = new DeleteInvoiceCommand(id);

            if (input.IsInvalid())
            {
                return BadRequest(input.ErrosList());
            }

            var output = await _mediator.Send(input, cancellationToken);

            if (output.IsValid())
            {
                return Ok();
            }

            return NoContent();
        }
    }
}
