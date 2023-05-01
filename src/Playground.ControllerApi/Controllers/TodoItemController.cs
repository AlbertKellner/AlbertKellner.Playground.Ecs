using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Playground.Application.Features.ToDoItems.Create.Models;
using Playground.Application.Features.ToDoItems.Delete.Models;
using Playground.Application.Features.ToDoItems.GetAll.Models;
using Playground.Application.Features.ToDoItems.GetById.Models;
using Playground.Application.Features.ToDoItems.IsCompleted.Models;
using Playground.Application.Features.ToDoItems.PatchTaskName.Models;
using Playground.Application.Features.ToDoItems.Update.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
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
                //Adicionar logs com o padr�o API_ClassName_M�todo => inputModel => TipoDeOcorrenciaComMessage

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
                //Adicionar logs com o padr�o API_ClassName_M�todo => inputModel => TipoDeOcorrenciaComMessage

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
                // Adicionar logs com o padr�o API_ClassName_M�todo => inputModel => TipoDeOcorrenciaComMessage

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

        [HttpPatch("{id:long}/task-name/{taskName}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PatchTaskNameAsync(
            [FromRoute] long id,
            [FromRoute] string taskName,
            CancellationToken cancellationToken)
        {
            var input = new PatchTaskNameToDoItemInput();
            input.SetId(id);
            input.SetTaskName(taskName);

            if (input.IsInvalid())
            {
                // Adicionar logs com o padr�o API_ClassName_M�todo => inputModel => TipoDeOcorrenciaComMessage

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

        [HttpPatch("{id:long}/is-completed/{isCompleted}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PatchIsCompletedAsync(
            [FromRoute] long id,
            [FromRoute] bool isCompleted,
            CancellationToken cancellationToken)
        {
            var input = new IsCompletedToDoItemInput();
            input.SetId(id);
            input.SetIsCompleted(isCompleted);

            if (input.IsInvalid())
            {
                // Adicionar logs com o padr�o API_ClassName_M�todo => inputModel => TipoDeOcorrenciaComMessage

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

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(
            [FromRoute] long id,
            CancellationToken cancellationToken)
        {
            var input = new DeleteToDoItemInput(id);

            if (input.IsInvalid())
            {
                // Adicionar logs com o padr�o API_ClassName_M�todo => inputModel => TipoDeOcorrenciaComMessage

                return BadRequest(input.ErrosList());
            }

            var output = await _mediator.Send(input, cancellationToken);

            if (output != null && output.IsDeleted)
            {
                return NoContent();
            }

            // Adicionar log de erro

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
