using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Playground.Application.Shared.Domain.OpenAi
{
    public class ChatGptRequestDto
    {
        [JsonPropertyName("model")]
        public string Model { get; set; } = "gpt-3.5-turbo";

        [JsonPropertyName("temperature")]
        public float Temperature { get; set; } = 1f;

        [JsonPropertyName("messages")]
        public IEnumerable<ChatGptMessageDto> Messages { get; set; } = Enumerable.Empty<ChatGptMessageDto>();
    }
}
