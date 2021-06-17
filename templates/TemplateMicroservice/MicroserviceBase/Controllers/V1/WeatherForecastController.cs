using MassTransit;
using MassTransit.Mediator;
using MicroserviceBase.Domain.Commands;
using MicroserviceBase.Domain.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace MicroserviceBase.Controllers.V1
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ISendEndpointProvider _sendEndpoint;
        private readonly IMediator _mediator;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IPublishEndpoint publishEndpoint
            , ISendEndpointProvider sendEndpointProvider, IMediator mediator)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
            _sendEndpoint = sendEndpointProvider;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAsync()
        {
            _logger.LogInformation(Request.Headers["x-correlation-id"]);
            await _publishEndpoint.Publish(new CustomerCreatedEvent() { Description = "sample event" });
            await _sendEndpoint.Send(new AddCustomerCommand() { Nome = "sample command" });
            await _mediator.Publish((new CustomerCreatedEvent() { Description = "mediator sample" }));
            return Ok();
        }
    }
}
