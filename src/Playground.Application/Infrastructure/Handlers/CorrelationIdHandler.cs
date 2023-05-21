using Playground.Application.Shared.AsyncLocals;

namespace Playground.Application.Infrastructure.Handlers
{
    public class CorrelationIdHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("CorrelationId", CorrelationContext.GetCorrelationId().ToString());

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
