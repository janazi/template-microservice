using MassTransit;
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
        private readonly IPublishEndpoint publishEndpoint;
        private readonly ISendEndpoint sendEndpoint;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IPublishEndpoint publishEndpoint, ISendEndpoint sendEndpoint)
        {
            _logger = logger;
            this.publishEndpoint = publishEndpoint;
            this.sendEndpoint = sendEndpoint;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAsync()
        {
            _logger.LogInformation(Request.Headers["x-correlation-id"]);
            await publishEndpoint.Publish(new TestEvent() { Description = "sample event" });
            await sendEndpoint.Send(new AddCustomerCommand() { Nome = "sample command" });

            return Ok();
        }
    }
}
