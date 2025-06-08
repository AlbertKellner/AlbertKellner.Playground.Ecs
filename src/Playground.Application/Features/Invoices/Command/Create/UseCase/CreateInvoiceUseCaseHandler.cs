using MediatR;
using Playground.Application.Features.Invoices.Command.Create.Models;

namespace Playground.Application.Features.Invoices.Command.Create.UseCase
{
    public class CreateInvoiceUseCaseHandler : IRequestHandler<CreateInvoiceCommand, CreateInvoiceOutput>
    {
        public async Task<CreateInvoiceOutput> Handle(CreateInvoiceCommand input, CancellationToken cancellationToken)
        {
            return new CreateInvoiceOutput
            {
                Id = 1,
                Date = input.Date,
                Value = input.Value,
                Description = input.Description
            };
        }
    }
}
