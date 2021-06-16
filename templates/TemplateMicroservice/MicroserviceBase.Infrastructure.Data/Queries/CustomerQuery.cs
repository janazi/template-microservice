using MicroserviceBase.Domain.Entities;
using MicroserviceBase.Domain.Queries;
using System.Collections.Generic;
using System.Linq;

namespace MicroserviceBase.Infrastructure.Data.Queries
{
    public class CustomerQuery : ICustomerQuery
    {
        private readonly IList<Customer> customers = new List<Customer>();
        public Customer GetByCPF(string cpf)
        {
            return customers.Where(c => c.CPF == cpf).SingleOrDefault();
        }
    }
}
