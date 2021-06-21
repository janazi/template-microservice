using MicroserviceBase.Domain.Commands.Customers;
using MicroserviceBase.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MicroserviceBase.Controllers.V1
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class CustomersController : ControllerBase
    {
        public CustomersController()
        {

        }

        [HttpPatch("{id}")]
        public IActionResult Patch(Guid customerId, [FromBody] UpdateCustomerCommand command)
        {
            var customer = new Customer("Nome", new DateTime(2010, 01, 01), "11111111111");

            customer.Patch(command);

            if (!customer.IsValid)
                return BadRequest();

            return new OkResult();
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateCustomerCommand CreateCustomerCommand)
        {
            return Ok();
        }

    }
}
