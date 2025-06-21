using System.Text.Json.Serialization;

namespace Playground.Application.Shared.Domain.OpenAi
{
    public class ChatGptMessageDto
    {
        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;
    }
}
