using DomainValidationCore.Validation;
using MicroserviceBase.Application.Specifications;
using MicroserviceBase.Domain.Commands.Customers;

namespace MicroserviceBase.Application.Validations.Customers
{
    public class AddCustomerValidation : Validator<AddCustomerCommand>
    {
        public AddCustomerValidation()
        {
            Add("NomeEstaPreenchido", new Rule<AddCustomerCommand>(new NomeEstaPreenchido(), "Preencher o campo nome"));
            Add("CustomerTemIdadeCompativel", new Rule<AddCustomerCommand>(new CustomerTemIdadeCompativel(), "Cliente de possuir mais de 18 anos"));
        }
    }
}
