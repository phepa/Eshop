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
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumers(assembly);

                x.UsingRabbitMq((context, rabbit) =>
                {
                    rabbit.Host("amqp://guest:guest@localhost:5672");
                    rabbit.ConfigureEndpoints(context);
                    rabbit.UseJsonSerializer();
                    rabbit.UseMessageRetry(cfg => cfg.Incremental(5, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10)));
                });
            });

            return services;
        }
    }
}
