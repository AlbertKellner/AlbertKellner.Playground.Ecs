using MediatR;

namespace Playground.Application.Features.Country.Query.GetAll.Models
{
    public class GetAllCountryQuery : IRequest<IEnumerable<GetAllCountryOutput>>
    {
        public IEnumerable<string> ErrosList()
        {
            return new List<string>();
        }

        public bool IsInvalid() => ErrosList().Any();

        public string FormattedErrosList() => $"({string.Join("|", ErrosList())})";
    }
}
