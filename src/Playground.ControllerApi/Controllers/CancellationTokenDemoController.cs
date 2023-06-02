using Microsoft.AspNetCore.Mvc;
using Playground.Application.Features.Pokemon.GetByName.Models;
using System.Net;

namespace Playground.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("cancellation-token-demo")]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public class CancellationTokenDemoController : ControllerBase
    {
        private readonly ILogger<CancellationTokenDemoController> _logger;
        public const long Size = 65000;

        public CancellationTokenDemoController(
            ILogger<CancellationTokenDemoController> logger)
        {
            _logger = logger;
        }

        [HttpGet("with")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public IActionResult WithCancellationTokenDemo(
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Api][CancellationTokenDemoController][WithCancellationTokenDemo][Start] Iniciando Execução");

            try
            {
                IxJAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning($"[Api][CancellationTokenDemoController][WithCancellationTokenDemo][OperationCanceledException] Execução interrompida");

                return NoContent();
            }
            finally
            {
                GC.Collect();
            }

            _logger.LogInformation($"[Api][CancellationTokenDemoController][WithCancellationTokenDemo][Ok] Execução completa");

            return Ok("Operation Completed Successfully");
        }

        [HttpGet("without")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public IActionResult WithoutCancellationTokenDemo()
        {
            _logger.LogInformation($"[Api][CancellationTokenDemoController][WithoutCancellationTokenDemo][Start] Iniciando Execução");

            try
            {
                IxJ();
            }
            finally
            {
                GC.Collect();
            }

            _logger.LogInformation($"[Api][CancellationTokenDemoController][WithoutCancellationTokenDemo][Ok] Execução completa");

            return Ok("Operation Completed Successfully");
        }

        private static void IxJAsync(CancellationToken cancellationToken)
        {
            var array = new long[Size, Size];
            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    array[i, j] = 1;

                    cancellationToken.ThrowIfCancellationRequested();
                }
            }
        }

        private static void IxJ()
        {
            var array = new long[Size, Size];
            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    array[i, j] = 1;
                }
            }
        }

        public static void JxI()
        {
            var array = new long[Size, Size];
            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    array[j, i] = 1;
                }
            }
        }
    }
}
