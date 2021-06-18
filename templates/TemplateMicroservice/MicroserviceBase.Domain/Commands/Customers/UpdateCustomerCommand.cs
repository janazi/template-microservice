namespace MicroserviceBase.Domain.Commands.Customers
{
    public class UpdateCustomerCommand
    {
        public string Nome { get; init; }

        public UpdateCustomerCommand(string nome)
        {
            Nome = nome;
        }
    }
}
