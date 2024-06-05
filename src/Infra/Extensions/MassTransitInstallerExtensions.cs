using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;
using MassTransit;

namespace Worker.Extensions;

[ExcludeFromCodeCoverage]
public static class MassTransitInstallerExtensions
{
    public static IServiceCollection AddMassTransitKafka(this IServiceCollection services, IConfiguration configuration,
            Action<IRiderRegistrationConfigurator> addConsumers, 
            Action<IRiderRegistrationContext, IKafkaFactoryConfigurator, string?> confiureConsumers) =>
        services
            .AddMassTransit(bus =>
            {
                bus.UsingInMemory();

                bus.AddRider(rider =>
                {
                    addConsumers(rider);

                    rider.UsingKafka((context, kafka) =>
                    {
                        kafka.Host(configuration.GetSection("Kafka:Host").Value);
                        var consumerGroup = configuration.GetSection("Kafka:ConsumerGroup").Value;

                        confiureConsumers(context, kafka, consumerGroup);
                    });
                });
            });
}
