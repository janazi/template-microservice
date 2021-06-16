using DomainValidationCore.Interfaces.Specification;
using MicroserviceBase.Domain.Commands;
using System;

namespace NeonModelo.Api.Specifications.Customers
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
