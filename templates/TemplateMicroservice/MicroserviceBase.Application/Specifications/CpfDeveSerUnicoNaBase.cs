using DomainValidationCore.Interfaces.Specification;
using MicroserviceBase.Domain.Commands.Customers;
using MicroserviceBase.Domain.Queries;
using MicroserviceBase.Infrastructure.CrossCutting;

namespace MicroserviceBase.Application.Specifications
{
    public class CpfDeveSerUnicoNaBase : ISpecification<AddCustomerCommand>
    {
        public bool IsSatisfiedBy(AddCustomerCommand c)
        {
            var customerQuery = DependencyResolver.GetService<ICustomerQuery>();
            var existingCustomer = customerQuery.GetByCPF(c.CPF);
            return existingCustomer is null;
        }
    }
}
