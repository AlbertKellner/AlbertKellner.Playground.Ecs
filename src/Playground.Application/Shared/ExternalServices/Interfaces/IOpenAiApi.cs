using Playground.Application.Shared.Domain.OpenAi;

namespace Playground.Application.Shared.ExternalServices.Interfaces
{
    public interface IOpenAiApi
    {
        Task<ChatGptResponseDto> SendPromptAsync(ChatGptRequestDto request, CancellationToken cancellationToken);
    }
}
