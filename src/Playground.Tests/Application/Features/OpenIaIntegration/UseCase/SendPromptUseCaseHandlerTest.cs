using Moq;
using Microsoft.Extensions.Logging;
using Playground.Application.Features.OpenIaIntegration.Command.SendPrompt.UseCase;
using Playground.Application.Features.OpenIaIntegration.Command.SendPrompt.Models;
using Playground.Application.Shared.ExternalServices.Interfaces;
using Playground.Application.Shared.Domain.OpenAi;

namespace Playground.Tests.Controllers
{
    public class SendPromptUseCaseHandlerTest
    {
        private readonly Mock<IOpenAiApi> _mockOpenAiApi;
        private readonly Mock<ILogger<SendPromptUseCaseHandler>> _mockLogger;
        private readonly SendPromptUseCaseHandler _handler;

        public SendPromptUseCaseHandlerTest()
        {
            _mockOpenAiApi = new Mock<IOpenAiApi>();
            _mockLogger = new Mock<ILogger<SendPromptUseCaseHandler>>();
            _handler = new SendPromptUseCaseHandler(_mockOpenAiApi.Object, _mockLogger.Object);
        }

        [Fact(DisplayName = "Handle QuandoExecutado DeveRetornarResposta")]
        public async Task Handle_QuandoExecutado_DeveRetornarResposta()
        {
            var command = new SendPromptCommand { Prompt = "hi", Model = "gpt-3.5-turbo", Temperature = 0.7f };
            var apiResponse = new ChatGptResponseDto
            {
                Choices = new List<ChatGptChoiceDto>
                {
                    new ChatGptChoiceDto { Message = new ChatGptMessageDto { Content = "hello" } }
                }
            };

            _mockOpenAiApi
                .Setup(api => api.SendPromptAsync(It.IsAny<ChatGptRequestDto>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(apiResponse);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Equal("hello", result.Response);
            _mockOpenAiApi.Verify(api => api.SendPromptAsync(It.IsAny<ChatGptRequestDto>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
