using DomainValidationCore.Interfaces.Specification;
using MicroserviceBase.Domain.Commands;

namespace MicroserviceBase.Application.Specifications
{
    public class NomeEstaPreenchido : ISpecification<AddCustomerCommand>
    {
        public bool IsSatisfiedBy(AddCustomerCommand c)
        {
            return !string.IsNullOrEmpty(c.Nome);
        }
    }
}
