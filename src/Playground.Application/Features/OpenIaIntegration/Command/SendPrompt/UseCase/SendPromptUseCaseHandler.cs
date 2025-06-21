using MediatR;
using Microsoft.Extensions.Logging;
using Playground.Application.Features.OpenIaIntegration.Command.SendPrompt.Models;
using Playground.Application.Shared.Domain.OpenAi;
using Playground.Application.Shared.ExternalServices.Interfaces;

namespace Playground.Application.Features.OpenIaIntegration.Command.SendPrompt.UseCase
{
    public class SendPromptUseCaseHandler : IRequestHandler<SendPromptCommand, SendPromptOutput>
    {
        private readonly IOpenAiApi _openAiApi;
        private readonly ILogger<SendPromptUseCaseHandler> _logger;

        public SendPromptUseCaseHandler(IOpenAiApi openAiApi, ILogger<SendPromptUseCaseHandler> logger)
        {
            _openAiApi = openAiApi;
            _logger = logger;
        }

        public async Task<SendPromptOutput> Handle(SendPromptCommand input, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[SendPromptUseCaseHandler][Handle] Enviando prompt para OpenAI");

            var request = new ChatGptRequestDto
            {
                Model = input.Model,
                Temperature = input.Temperature,
                Messages = new List<ChatGptMessageDto>
                {
                    new() { Role = "user", Content = input.Prompt }
                }
            };

            var response = await _openAiApi.SendPromptAsync(request, cancellationToken);

            var output = new SendPromptOutput
            {
                Response = response.Choices.FirstOrDefault()?.Message.Content ?? string.Empty
            };

            _logger.LogInformation("[SendPromptUseCaseHandler][Handle] Resposta obtida");

            return output;
        }
    }
}
