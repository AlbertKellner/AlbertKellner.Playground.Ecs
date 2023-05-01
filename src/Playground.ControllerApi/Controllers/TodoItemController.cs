using MediatR;
using Microsoft.AspNetCore.Mvc;
using Playground.Application.Features.ToDoItems.Create.Models;
using Playground.Application.Features.ToDoItems.GetAll.Models;
using Playground.Application.Features.ToDoItems.GetById.Models;
using Playground.Application.Features.ToDoItems.Update.Models;
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

        public ToDoItemController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CreateToDoItemOutput), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create(
            [FromBody] CreateToDoItemInput input,
            CancellationToken cancellationToken)
        {
            if (input.IsInvalid())
            {
                //Adicionar logs com o padrão API_ClassName_Método => inputModel => TipoDeOcorrenciaComMessage

                return BadRequest(input.ErrosList());
            }

            var output = await _mediator.Send(input, cancellationToken);

            if (output != null && output.IsCreated())
            {
                return CreatedAtRoute(
                    routeName: "GetById",
                    routeValues: new { id = output.Id },
                    value: output);
            }

            //Adicionar log de erro

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("{id:long}", Name = "GetById")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(GetByIdToDoItemOutput), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] long id,
            [FromQuery] GetByIdToDoItemInput input,
            CancellationToken cancellationToken)
        {
            input.SetId(id);

            if (input.IsInvalid())
            {
                //Adicionar logs com o padrão API_ClassName_Método => inputModel => TipoDeOcorrenciaComMessage

                return BadRequest(input.ErrosList());
            }

            var output = await _mediator.Send(input, cancellationToken);

            if (output == null)
            {
                //Adicionar log de erro

                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
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
            var output = await _mediator.Send(new GetAllToDoItemInput(), cancellationToken);

            if (output == null)
            {
                //Adicionar log de erro

                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
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
        public async Task<IActionResult> Update(
            [FromRoute] long id,
            [FromBody] UpdateToDoItemInput input,
            CancellationToken cancellationToken)
        {
            input.SetId(id);

            if (input.IsInvalid())
            {
                // Adicionar logs com o padrão API_ClassName_Método => inputModel => TipoDeOcorrenciaComMessage

                return BadRequest(input.ErrosList());
            }

            var output = await _mediator.Send(input, cancellationToken);

            if (output != null && output.IsUpdated())
            {
                return NoContent();
            }

            // Adicionar log de erro

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
