using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;
using MassTransit;
using Infra.Extensions;
using Application.Notifications.Motorcycles;
using Worker.Consumers.Motorcycles;

namespace Worker.Extensions;

[ExcludeFromCodeCoverage]
public static class MassTransitInstallerExtensions
{
    public static IServiceCollection AddConsumers(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddMassTransitKafka(configuration, rider =>
            {
                rider.AddConsumer<MotorcycleCreatedConsumer>();
            },
            (context, kafka, consumerGroup) =>
            {
                kafka.TopicEndpoint<MotorcycleCreatedMessage>(MotorcycleCreatedNotify.TopicName, consumerGroup, consumer =>
                {
                    consumer.ConfigureConsumer<MotorcycleCreatedConsumer>(context);
                    consumer.CreateIfMissing();
                });
            });
}
