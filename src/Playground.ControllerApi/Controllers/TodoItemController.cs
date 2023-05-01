using MediatR;
using Microsoft.AspNetCore.Mvc;
using Playground.Application.Features.ToDoItems.Create.Models;
using Playground.Application.Features.ToDoItems.GetById.Models;
using Playground.Models;
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
        [ProducesResponseType(typeof(GetByIdToDoItemOutput), StatusCodes.Status201Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
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

            if (output != null && output.IsCreated)
            {
                return CreatedAtRoute(
                    routeName: "GetById",
                    routeValues: new { id = output.Id },
                    value: output);
            }

            //Adicionar log de erro

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("{id:int}", Name = "GetById")]
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

            if (output.IsValid)
            {
                return Ok(output);
            }
                
            return NoContent();
        }
    }
}
