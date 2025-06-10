using Moq;
using Microsoft.Extensions.Logging;
using Playground.Application.Features.Country.Query.GetAll.UseCase;
using Playground.Application.Features.Country.Query.GetAll.Interface;
using Playground.Application.Features.Country.Query.GetAll.Models;

namespace Playground.Tests.Controllers
{
    public class HandleGetAllCountryUseCaseHandlerTest
    {
        private readonly Mock<IGetAllCountryRepository> _mockRepo;
        private readonly Mock<ILogger<GetAllCountryUseCaseHandler>> _mockLogger;
        private readonly GetAllCountryUseCaseHandler _handler;

        public HandleGetAllCountryUseCaseHandlerTest()
        {
            _mockRepo = new Mock<IGetAllCountryRepository>();
            _mockLogger = new Mock<ILogger<GetAllCountryUseCaseHandler>>();
            _handler = new GetAllCountryUseCaseHandler(_mockRepo.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Handle_QuandoExecutado_DeveRetornarLista()
        {
            var list = new List<GetAllCountryOutput> { new GetAllCountryOutput { Name = "Brazil" } };

            _mockRepo
                .Setup(r => r.GetAllCountryAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(list);

            var result = await _handler.Handle(new GetAllCountryQuery(), CancellationToken.None);

            Assert.Equal(list, result);
            _mockRepo.Verify(r => r.GetAllCountryAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
