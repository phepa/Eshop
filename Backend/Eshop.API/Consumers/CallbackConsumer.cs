using Eshop.Shared.Models.Messages;
using MassTransit;
using System.Text.Json;

namespace Eshop.API.Consumers
{
    public class CallbackConsumer : IConsumer<CallbackMessage>
    {
        private readonly ILogger<CallbackConsumer> _logger;

        public CallbackConsumer(ILogger<CallbackConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<CallbackMessage> context)
        {
            CallbackMessage message = context.Message;
            _logger.LogInformation("Callback arrived = " + JsonSerializer.Serialize(message));

            return Task.CompletedTask;
        }
    }
}
