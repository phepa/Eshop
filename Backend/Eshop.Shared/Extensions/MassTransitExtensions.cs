using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Eshop.Shared.Extensions
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddMassTransitMQ(this IServiceCollection services, Assembly assembly)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumers(assembly);

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("amqp://guest:guest@localhost:5672");
                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
