using System;
using DomainValidationCore.Interfaces.Specification;
using MicroserviceBase.Domain.Commands;

namespace MicroserviceBase.Application.Specifications
{
    public class CustomerTemIdadeCompativel : ISpecification<AddCustomerCommand>
    {
        public bool IsSatisfiedBy(AddCustomerCommand command)
        {
            var dataNascimentoMaiorDeIdade = DateTime.Now.AddYears(-18);
            return command.DataNascimento < dataNascimentoMaiorDeIdade;
        }
    }
}
