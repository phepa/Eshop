using Eshop.Shared.Helpers;
using Eshop.Shared.Models.Messages;
using MassTransit;
using System.Text.Json;

namespace Eshop.Service.Consumers
{
    public class NotificationConsumer : IConsumer<NotificationMessage>
    {
        private readonly ILogger<NotificationConsumer> _logger;
        private readonly PersistanceHelper _persistanceHelper;

        public NotificationConsumer(ILogger<NotificationConsumer> logger, PersistanceHelper persistanceHelper)
        {
            _logger = logger;
            _persistanceHelper = persistanceHelper;
        }

        public Task Consume(ConsumeContext<NotificationMessage> context)
        {
            NotificationMessage message = context.Message;
            _logger.LogInformation("Notification arrived = " + JsonSerializer.Serialize(message));

            if (message.Notification.NotNull())
            {
                _persistanceHelper.AddNotificaton(message.Id, message.Notification);
            }

            return Task.CompletedTask;
        }
    }
}
