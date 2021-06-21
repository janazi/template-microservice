using MediatR;
using MicroserviceBase.Domain.Commands.Customers;
using MicroserviceBase.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace MicroserviceBase.Application.Handlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly ILogger<CreateCustomerCommandHandler> logger;

        public CreateCustomerCommandHandler(ILogger<CreateCustomerCommandHandler> logger)
        {
            this.logger = logger;
        }
        public Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            logger.LogDebug("Entering Create Customer Handler");
            logger.LogInformation(request.Nome);

            var customer = new Customer(request.Nome, request.DataNascimento, request.CPF);
            if (customer.IsValid is false)
                return Task.FromResult(customer);

            //TODO: INSERT
            return Task.FromResult(customer);
        }
    }
}
