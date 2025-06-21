using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Playground.Application.Shared.Domain.OpenAi
{
    public class ChatGptResponseDto
    {
        [JsonPropertyName("choices")]
        public List<ChatGptChoiceDto> Choices { get; set; } = new();
    }

    public class ChatGptChoiceDto
    {
        [JsonPropertyName("message")]
        public ChatGptMessageDto Message { get; set; } = new();
    }
}
