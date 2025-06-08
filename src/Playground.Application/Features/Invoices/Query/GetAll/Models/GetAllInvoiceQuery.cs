using MediatR;

namespace Playground.Application.Features.Invoices.Query.GetAll.Models
{
    public class GetAllInvoiceQuery : IRequest<IEnumerable<GetAllInvoiceOutput>>
    {
        public IEnumerable<string> ErrosList()
        {
            return new List<string>();
        }

        public bool IsInvalid() => ErrosList().Any();

        public string FormattedErrosList() => $"({string.Join("|", ErrosList())})";
    }
}
