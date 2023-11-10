using Eshop.Shared.Models.Messages;
using MassTransit;

namespace Eshop.API.Publishers
{
    public class NotificationPublisher
    {
        private readonly ILogger<NotificationPublisher> _logger;
        private readonly IBus _bus;

        public NotificationPublisher(ILogger<NotificationPublisher> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        public async Task SendNotification(NotificationMessage notification)
        {
            await _bus.Publish(notification);
        }
    }
}
