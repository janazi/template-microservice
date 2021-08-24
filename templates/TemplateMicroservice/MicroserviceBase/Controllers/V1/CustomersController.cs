using MediatR;
using MicroserviceBase.Domain.Commands.Customers;
using MicroserviceBase.Domain.Entities;
using MicroserviceBase.Infrastructure.CrossCutting.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MicroserviceBase.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class CustomersController : ControllerBase, IWarmUpController
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        public IActionResult Patch([FromBody] UpdateCustomerCommand command)
        {
            var customer = new Customer(command.Nome, command.DataNascimento, command.CPF);

            if (customer.IsValid is false)
                return BadRequest(customer);

            customer.Patch(command);

            if (customer.IsValid is false)
                return NoContent();

            return new AcceptedResult(nameof(Patch), customer);
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

        [HttpGet, Route("WarmUp")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult WarmUp()
        {
            return Ok("Warmed Up");
        }
    }
}
