using MediatR;
using MicroserviceBase.Domain.Commands.Customers;
using MicroserviceBase.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        public IActionResult Patch(Guid customerId, [FromBody] UpdateCustomerCommand command)
        {
            var customer = new Customer("Nome", new DateTime(2010, 01, 01), "11111111111");

            customer.Patch(command);

            if (customer.IsValid is false)
                return NoContent();

            return new AcceptedResult("Patch", customer);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsValid is false)
                return BadRequest(result.Notifications);

            return CreatedAtAction(nameof(Post), result);
        }

    }
}
