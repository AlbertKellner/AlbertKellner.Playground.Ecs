using MediatR;
using Playground.Application.Features.Invoices.Command.Delete.Models;

namespace Playground.Application.Features.Invoices.Command.Delete.UseCase
{
    public class DeleteInvoiceUseCaseHandler : IRequestHandler<DeleteInvoiceCommand, DeleteInvoiceOutput>
    {
        public async Task<DeleteInvoiceOutput> Handle(DeleteInvoiceCommand input, CancellationToken cancellationToken)
        {
            return new DeleteInvoiceOutput();
        }
    }
}
