using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;
using MassTransit;
using Application.Notifications.Motorcycles;
using Worker.Consumers.Motorcycles;

namespace Worker.Extensions;

[ExcludeFromCodeCoverage]
public static class MassTransitInstallerExtensions
{
    public static IServiceCollection AddMassTransitKafka(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddMassTransit(bus =>
            {
                bus.UsingInMemory();
                
                bus.AddRider(rider =>
                {                    
                    rider.AddConsumer<MotorcycleCreatedConsumer>();

                    rider.UsingKafka((context, kafka) =>
                    {
                        kafka.Host(configuration.GetSection("Kafka:Host").Value);
                        var consumerGroup = configuration.GetSection("Kafka:ConsumerGroup").Value;

                        kafka.TopicEndpoint<MotorcycleCreatedMessage>(MotorcycleCreatedNotify.TopicName, consumerGroup, consumer =>
                        {
                            consumer.ConfigureConsumer<MotorcycleCreatedConsumer>(context);
                            consumer.CreateIfMissing();
                        });
                    });
                });
            });
}
