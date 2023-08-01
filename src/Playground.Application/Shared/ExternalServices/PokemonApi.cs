using Microsoft.Extensions.Logging;
using Playground.Application.Shared.Domain.ApiDto;
using Playground.Application.Shared.ExternalServices.Interfaces;
using Refit;
using Polly;
using Polly.Timeout;
using System.Net;
using Microsoft.Extensions.Caching.Memory;
using Playground.Application.Infrastructure.Configuration;
using Playground.Application.Infrastructure.Extensions;
using Playground.Application.Shared.AsyncLocals;

namespace Playground.Application.Shared.ExternalServices
{
    internal class PokemonApi : IPokemonApi
    {
        private readonly ILogger<IPokemonApi> _logger;
        private readonly IPokemonApi _pokemonApi;
        private readonly IAsyncPolicy _policyWrap;
        private readonly IMemoryCache _memoryCache;
        private readonly ExternalApiOptions _externalApiOptions;

        public PokemonApi(
            ILogger<IPokemonApi> logger,
            IMemoryCache memoryCache,
            ExternalApiOptions externalApiOptions)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _externalApiOptions = externalApiOptions;

            var httpClient = new HttpClient { BaseAddress = new Uri(externalApiOptions.PokemonApiUrl) }; //TODO: Otimizar
            httpClient.DefaultRequestHeaders.Add("CorrelationId", CorrelationContext.GetCorrelationId().ToString());
            _pokemonApi = RestService.For<IPokemonApi>(httpClient);

            var retryPolicy = Policy
                .Handle<ApiException>(exception => exception.StatusCode is HttpStatusCode.NoContent)
                .WaitAndRetryAsync(externalApiOptions.PokemonApiRetryCount, retryAttempt 
                    => TimeSpan.FromSeconds(externalApiOptions.PokemonApiSleepDuration * retryAttempt));

            var timeoutPolicy = Policy.TimeoutAsync(externalApiOptions.PokemonApiTimeout, TimeoutStrategy.Pessimistic);

            _policyWrap = Policy.WrapAsync(retryPolicy, timeoutPolicy);
        }

        public async Task<PokemonOutApiDto> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[Shared][PokemonApi][GetByNameAsync][Start] input:({@pokemonName})", name);

            return await _memoryCache.GetOrCreateAsync($"{name}-{CorrelationContext.GetCorrelationId()}", async entry =>
            {
                PokemonOutApiDto attemptResult = new();
                PokemonOutApiDto apiResult = new();

                try
                {
                    int attempt = 1;
                    attemptResult = await _policyWrap.ExecuteAsync(async (ct) =>
                    {
                        _logger.Log(attempt == 1 ? LogLevel.Information : LogLevel.Warning,
                                    "[Shared][PokemonApi][GetByNameAsync][Attempt {@attemptNumber}] input:({pokemonName})", attempt++, name);

                        apiResult = await _pokemonApi.GetByNameAsync(name, ct);

                        return apiResult;
                    }, cancellationToken);
                }
                catch (TaskCanceledException exception)
                {
                    _logger.LogError(exception, $"[Shared][PokemonApi][GetByNameAsync][Timeout] input:({name})");

                    throw;
                }
                catch (ApiException exception) when
                    (exception.StatusCode is HttpStatusCode.NoContent)
                {
                    _logger.LogWarning($"[Shared][PokemonApi][GetByNameAsync][NoContent] input:({name})");
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
                    _logger.LogTroubleshooting($"[Troubleshooting][Shared][PokemonApi][GetByNameAsync] url:{_externalApiOptions.PokemonApiUrl}");
                    _logger.LogTroubleshooting($"[Troubleshooting][Shared][PokemonApi][GetByNameAsync] apiResult:{apiResult.ToTroubleshooting()}");

                    entry.SetAbsoluteExpiration(attemptResult != null ? TimeSpan.FromSeconds(10) : TimeSpan.FromSeconds(5)); //TODO: Extract to configJson
                }

                _logger.LogInformation("[Shared][PokemonApi][GetByNameAsync][Ok] input:({@name})", name);

                return attemptResult ?? new();
            }) ?? new();
        }

    }
}
