using Microsoft.Extensions.Logging;
using Playground.Application.Shared.Domain.ApiDto;
using Playground.Application.Shared.ExternalServices.Interfaces;
using Refit;
using Polly;
using Polly.Timeout;
using System.Net;
using Microsoft.Extensions.Caching.Memory;
using Playground.Application.Infrastructure.Configuration;

namespace Playground.Application.Shared.ExternalServices
{
    internal class PokemonApi : IPokemonApi
    {
        private readonly ILogger<IPokemonApi> _logger;
        private readonly IPokemonApi _pokemonApi;
        private readonly IAsyncPolicy _policyWrap;
        private readonly IMemoryCache _memoryCache;

        public PokemonApi(
            ILogger<IPokemonApi> logger,
            IMemoryCache memoryCache,
            ExternalApiOptions apiOptions)
        {
            _logger = logger;
            _memoryCache = memoryCache;

            _pokemonApi = RestService.For<IPokemonApi>(apiOptions.PokemonApiUrl);

            var retryPolicy = Policy
                .Handle<ApiException>(exception => exception.StatusCode is HttpStatusCode.NotFound)
                .WaitAndRetryAsync(apiOptions.PokemonApiRetryCount, retryAttempt => TimeSpan.FromSeconds(apiOptions.PokemonApiSleepDuration * retryAttempt));

            var timeoutPolicy = Policy.TimeoutAsync(apiOptions.PokemonApiTimeout, TimeoutStrategy.Pessimistic);

            _policyWrap = Policy.WrapAsync(retryPolicy, timeoutPolicy);
        }

        public async Task<PokemonOutApiDto> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[Shared][PokemonApi][GetByNameAsync][Start] input:({name})");

            return await _memoryCache.GetOrCreateAsync(name, async entry =>
            {
                PokemonOutApiDto attemptResult = new();

                try
                {
                    int attempt = 1;
                    attemptResult = await _policyWrap.ExecuteAsync(async (ct) =>
                    {
                        _logger.Log(attempt == 1 ? LogLevel.Information : LogLevel.Warning,
                                    $"[Shared][PokemonApi][GetByNameAsync][Attempt {attempt++}] input:({name})");

                        return await _pokemonApi.GetByNameAsync(name, ct);
                    }, cancellationToken);
                }
                catch (TaskCanceledException exception)
                {
                    _logger.LogError(exception, $"[Shared][PokemonApi][GetByNameAsync][Timeout] input:({name})");

                    throw;
                }
                catch (ApiException exception) when
                    (exception.StatusCode is HttpStatusCode.NotFound)
                {
                    _logger.LogWarning($"[Shared][PokemonApi][GetByNameAsync][NotFound] input:({name})");
                }
                catch (ApiException exception) when
                    (exception.StatusCode is HttpStatusCode.InternalServerError)
                {
                    _logger.LogError(exception, $"[Shared][PokemonApi][GetByNameAsync][InternalServerError] input:({name})");

                    throw;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, $"[Shared][PokemonApi][GetByNameAsync][UntrackedError] input:({name})");

                    throw;
                }
                finally
                {
                    entry.SetAbsoluteExpiration(attemptResult != null ? TimeSpan.FromSeconds(10) : TimeSpan.FromSeconds(5));
                }

                _logger.LogInformation($"[Shared][PokemonApi][GetByNameAsync][Ok] input:({name})");

                return attemptResult ?? new();
            }) ?? new();
        }

    }
}
