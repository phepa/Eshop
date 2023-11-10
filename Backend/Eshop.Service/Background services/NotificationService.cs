namespace Eshop.Service.Background_services
{
    public class NotificationService : BackgroundService
    {
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(ILogger<NotificationService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("{Source} started at {DateTime}", nameof(NotificationService), DateTime.UtcNow);

                await Task.Delay(1000);
            }
        }
    }
}
