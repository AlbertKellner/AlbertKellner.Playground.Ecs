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
        private readonly ILogger<PokemonApi> _logger;
        private readonly IPokemonApi _pokemonApi;
        private readonly IAsyncPolicy _policyWrap;
        private readonly IMemoryCache _memoryCache;
        private readonly ExternalApiOptions _externalApiOptions;

        public PokemonApi(
            ILogger<PokemonApi> logger,
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
            return await _memoryCache.GetOrCreateAsync($"{name}", async entry =>
            {
                PokemonOutApiDto attemptResult = new();
                PokemonOutApiDto apiResult = new();

                try
                {
                    int attempt = 1;
                    attemptResult = await _policyWrap.ExecuteAsync(async (ct) =>
                    {
                        _logger.Log(attempt == 1 ? LogLevel.Information : LogLevel.Warning,
                            "[PokemonApi][GetByNameAsync] Consultando API, Tentativa: {@attemptNumber}. input:({pokemonName})", attempt++, name);

                        apiResult = await _pokemonApi.GetByNameAsync(name, ct);

                        return apiResult;
                    }, cancellationToken);
                }
                catch (TaskCanceledException exception)
                {
                    HandleTaskCanceledException(exception, name);
                }
                catch (ApiException exception)
                {
                    HandleApiException(exception, name);
                }
                catch (Exception exception)
                {
                    HandleGenericException(exception, name);
                }
                finally
                {
                    _logger.LogTroubleshooting($"[Troubleshooting][PokemonApi][GetByNameAsync] url:{_externalApiOptions.PokemonApiUrl}");
                    _logger.LogTroubleshooting($"[Troubleshooting][PokemonApi][GetByNameAsync] apiResult:{apiResult.ToTroubleshooting()}");

                    entry.SetAbsoluteExpiration(attemptResult != null ? TimeSpan.FromSeconds(3) : TimeSpan.FromSeconds(1)); //TODO: Extract to configJson
                }

                _logger.LogInformation("[PokemonApi][GetByNameAsync] Consulta realizada com sucesso. input:({@name})", name);

                return attemptResult ?? new();
            }) ?? new();
        }

        private void HandleTaskCanceledException(TaskCanceledException exception, string name)
        {
            _logger.LogError(exception, $"[PokemonApi][GetByNameAsync] Erro de Timeout. input:({name})");

            throw exception;
        }

        private void HandleApiException(ApiException exception, string name)
        {
            if (exception.StatusCode == HttpStatusCode.NoContent)
            {
                _logger.LogWarning($"[PokemonApi][GetByNameAsync] Nenhum dado retornou da API. input:({name})");
                return;
            }

            if (exception.StatusCode == HttpStatusCode.InternalServerError)
            {
                _logger.LogError(exception, $"[PokemonApi][GetByNameAsync] Erro de integração. Erro: {exception.InnerException}. input:({name})");
                throw exception;
            }

            _logger.LogError(exception, $"[PokemonApi][GetByNameAsync] Erro de integração. StatusCode: {exception.StatusCode}. input:({name})");
            throw exception;
        }

        private void HandleGenericException(Exception exception, string name)
        {
            _logger.LogError(exception, $"[PokemonApi][GetByNameAsync] Erro desconhecido input:({name})");
            throw exception;
        }
    }
}

