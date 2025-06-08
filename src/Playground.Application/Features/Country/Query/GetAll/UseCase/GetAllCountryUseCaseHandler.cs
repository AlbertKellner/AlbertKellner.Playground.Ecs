using MediatR;
using Microsoft.Extensions.Logging;
using Playground.Application.Features.Country.Query.GetAll.Interface;
using Playground.Application.Features.Country.Query.GetAll.Models;

namespace Playground.Application.Features.Country.Query.GetAll.UseCase
{
    public class GetAllCountryUseCaseHandler : IRequestHandler<GetAllCountryQuery, IEnumerable<GetAllCountryOutput>>
    {
        private readonly IGetAllCountryRepository _getAllCountryRepository;
        private readonly ILogger<GetAllCountryUseCaseHandler> _logger;

        public GetAllCountryUseCaseHandler(
            IGetAllCountryRepository getAllCountryRepository,
            ILogger<GetAllCountryUseCaseHandler> logger)
        {
            _getAllCountryRepository = getAllCountryRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<GetAllCountryOutput>> Handle(GetAllCountryQuery input, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[GetAllCountryUseCaseHandler][Handle] Iniciando consulta no repositorio de dados");

            var result = await _getAllCountryRepository.GetAllCountryAsync(cancellationToken);

            _logger.LogInformation($"[GetAllCountryUseCaseHandler][Handle] Retornando caso de uso");

            return result ?? new List<GetAllCountryOutput>();
        }
    }
}
