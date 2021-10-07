using MicroserviceBase.Domain.Entities;
using MicroserviceBase.Domain.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviceBase.Infrastructure.Data.Queries
{
    public class CustomerQuery : ICustomerQuery
    {
        private static readonly List<Customer> customers = new();
        public Task<Customer> GetByCpf(string cpf)
        {
            return Task.FromResult(customers.Where(c => c.CPF == cpf).SingleOrDefault());
        }
    }
}
