using Eshop.Shared.Helpers;
using Eshop.Shared.Models.Messages;
using MassTransit;

namespace Eshop.Service.Background_services
{
    public class NotificationService : BackgroundService
    {
        private readonly ILogger<NotificationService> _logger;
        private readonly PersistanceHelper _persistanceHelper;
        private readonly IBus _bus;

        public NotificationService(ILogger<NotificationService> logger, PersistanceHelper persistanceHelper, IBus bus)
        {
            _logger = logger;
            _persistanceHelper = persistanceHelper;
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("{Source} started at {DateTime}", nameof(NotificationService), DateTime.UtcNow);

                string notification = _persistanceHelper.GetNotificaton();

                if (notification.NotNullOrEmpty())
                {
                    await _bus.Publish(new CallbackMessage()
                    {
                        Message = notification,
                    });
                }

                await Task.Delay(10000);
            }
        }
    }
}
