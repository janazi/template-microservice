using MassTransit;
using MicroserviceBase.Domain.Events;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MicroserviceBase.Infrastructure.Messaging
{
    public class MessageConsumer : IConsumer<CustomerCreatedEvent>
    {
        private readonly ILogger<MessageConsumer> _logger;

        public MessageConsumer(ILogger<MessageConsumer> logger)
        {
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<CustomerCreatedEvent> context)
        {
            _logger.LogInformation($"Message Consumed: {context.Message.Description}");
            await Task.CompletedTask;
        }
    }
}
