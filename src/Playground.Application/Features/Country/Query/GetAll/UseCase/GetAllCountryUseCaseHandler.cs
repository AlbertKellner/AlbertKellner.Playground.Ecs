using MediatR;
using Playground.Application.Features.Country.Command.Create.Interface;
using Playground.Application.Features.Country.Query.GetAll.Models;

namespace Playground.Application.Features.Country.Query.GetAll.UseCase
{
    public class GetAllCountryUseCaseHandler : IRequestHandler<GetAllCountryQuery, IEnumerable<GetAllCountryOutput>>
    {
        private readonly IGetAllCountryRepository _getAllCountryRepository;

        public GetAllCountryUseCaseHandler(IGetAllCountryRepository getAllCountryRepository)
        {
            _getAllCountryRepository = getAllCountryRepository;
        }

        public async Task<IEnumerable<GetAllCountryOutput>> Handle(GetAllCountryQuery input, CancellationToken cancellationToken)
        {
            return await _getAllCountryRepository.GetAllCountryAsync(cancellationToken) 
                ?? new List<GetAllCountryOutput>();
        }
    }
}
