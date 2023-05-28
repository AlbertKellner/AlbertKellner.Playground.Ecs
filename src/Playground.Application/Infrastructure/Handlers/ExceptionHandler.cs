using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Playground.Application.Infrastructure.Handlers
{
    public class ExceptionHandler<TRequest> : IRequestExceptionAction<TRequest, Exception> 
        where TRequest : notnull
    {
        private readonly ILogger<TRequest> _logger;

        public ExceptionHandler(
            ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task Execute(TRequest request, Exception exception, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            _logger.LogError(exception, $"[ExceptionHandler][Execute] type:{typeof(TRequest)} request:{request} exception:{exception.InnerException}");
        }
    }
}
