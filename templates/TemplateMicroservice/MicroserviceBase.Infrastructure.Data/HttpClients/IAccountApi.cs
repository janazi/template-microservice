using MicroserviceBase.Domain.Commands.Customers;
using MicroserviceBase.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using System.Threading.Tasks;

namespace MicroserviceBase.Infrastructure.Data.HttpClients
{
    public interface ICustomerApi
    {
        [Get("/v1/customer/")]
        Task<Customer> GetCustomer();

        [Post("/v1/customer")]
        Task<Customer> Create(AddCustomerCommand command);
    }

    public static class AccountApiExtension
    {
        public static void AddCustomerApi(this IServiceCollection services, string baseAddress)
        {
            services
                    .AddRefitClient<ICustomerApi>()
                    .ConfigureHttpClient(httpClient =>
                    {
                        httpClient.BaseAddress = new Uri(baseAddress);
                        httpClient.DefaultRequestVersion = new Version(2, 0);
                    });
        }
    }
}
