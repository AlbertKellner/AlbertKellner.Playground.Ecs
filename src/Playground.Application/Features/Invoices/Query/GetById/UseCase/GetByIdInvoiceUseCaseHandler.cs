using MediatR;
using Playground.Application.Features.Invoices.Query.GetById.Models;

namespace Playground.Application.Features.Invoices.Query.GetById.UseCase
{
    public class GetByIdInvoiceUseCaseHandler : IRequestHandler<GetByIdInvoiceQuery, GetByIdInvoiceOutput>
    {
        public async Task<GetByIdInvoiceOutput> Handle(GetByIdInvoiceQuery input, CancellationToken cancellationToken)
        {
            var invoices = new List<GetByIdInvoiceOutput>
            {
                new GetByIdInvoiceOutput
                {
                    Id = 1,
                    Date = DateTime.UtcNow,
                    Value = 100,
                    Description = "Invoice 1"
                },
                new GetByIdInvoiceOutput
                {
                    Id = 2,
                    Date = DateTime.UtcNow,
                    Value = 200,
                    Description = "Invoice 2"
                }
            };

            return invoices.SingleOrDefault(inv => inv.Id == input.Id) ?? new GetByIdInvoiceOutput();
        }
    }
}
