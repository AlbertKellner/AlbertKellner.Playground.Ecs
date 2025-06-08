using MediatR;
using Playground.Application.Features.Invoices.Command.Update.Models;

namespace Playground.Application.Features.Invoices.Command.Update.UseCase
{
    public class UpdateInvoiceUseCaseHandler : IRequestHandler<UpdateInvoiceCommand, UpdateInvoiceOutput>
    {
        public async Task<UpdateInvoiceOutput> Handle(UpdateInvoiceCommand input, CancellationToken cancellationToken)
        {
            return new UpdateInvoiceOutput
            {
                Id = input.Id,
                Date = input.Date,
                Value = input.Value,
                Description = input.Description
            };
        }
    }
}
