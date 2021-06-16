using MicroserviceBase.Domain.Entities;

namespace MicroserviceBase.Domain.Queries
{
    public interface ICustomerQuery
    {
        Customer GetByCPF(string cnpj);
    }
}
