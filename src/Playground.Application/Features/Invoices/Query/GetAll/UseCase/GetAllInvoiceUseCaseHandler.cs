using MediatR;
using Playground.Application.Features.Invoices.Query.GetAll.Models;

namespace Playground.Application.Features.Invoices.Query.GetAll.UseCase
{
    public class GetAllInvoiceUseCaseHandler : IRequestHandler<GetAllInvoiceQuery, IEnumerable<GetAllInvoiceOutput>>
    {
        public async Task<IEnumerable<GetAllInvoiceOutput>> Handle(GetAllInvoiceQuery input, CancellationToken cancellationToken)
        {
            var invoices = new List<GetAllInvoiceOutput>
            {
                new GetAllInvoiceOutput
                {
                    Id = 1,
                    Date = DateTime.UtcNow,
                    Value = 100,
                    Description = "Invoice 1"
                },
                new GetAllInvoiceOutput
                {
                    Id = 2,
                    Date = DateTime.UtcNow,
                    Value = 200,
                    Description = "Invoice 2"
                }
            };

            return invoices;
        }
    }
}
