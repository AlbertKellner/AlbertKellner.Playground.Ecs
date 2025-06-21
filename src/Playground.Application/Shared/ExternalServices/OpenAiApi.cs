using Microsoft.Extensions.Logging;
using Playground.Application.Infrastructure.Configuration;
using Playground.Application.Shared.AsyncLocals;
using Playground.Application.Shared.Domain.OpenAi;
using Playground.Application.Shared.ExternalServices.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Playground.Application.Shared.ExternalServices
{
    internal class OpenAiApi : IOpenAiApi
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<OpenAiApi> _logger;
        private readonly ExternalApiOptions _options;

        public OpenAiApi(ILogger<OpenAiApi> logger, ExternalApiOptions options)
        {
            _logger = logger;
            _options = options;

            _httpClient = new HttpClient { BaseAddress = new Uri(options.OpenIaApi.Url) };
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.OpenIaApi.ApiKey);
            _httpClient.DefaultRequestHeaders.Add("CorrelationId", CorrelationContext.GetCorrelationId().ToString());
        }

        public async Task<ChatGptResponseDto> SendPromptAsync(ChatGptRequestDto request, CancellationToken cancellationToken)
        {
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/chat/completions", content, cancellationToken);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<ChatGptResponseDto>(responseJson) ?? new ChatGptResponseDto();
        }
    }
}
