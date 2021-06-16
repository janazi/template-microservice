using System;

namespace MicroserviceBase.Domain.Commands
{
    public class AddCustomerCommand
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
    }
}
