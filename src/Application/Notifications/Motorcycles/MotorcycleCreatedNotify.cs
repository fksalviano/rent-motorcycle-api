using Application.Notifications.Motorcycles.Abstractions;
using MassTransit;
using Domain.Model;
using Microsoft.Extensions.Logging;

namespace Application.Notifications.Motorcycles;

public class MotorcycleCreatedNotify : IMotorcycleCreatedNotify
{    
    private readonly ITopicProducer<MotorcycleCreatedMessage> _topicProducer;
    private readonly IBusControl _busControl;

    private readonly ILogger<MotorcycleCreatedNotify> _logger;

    public const string TopicName = "motorcycle-created";

    public MotorcycleCreatedNotify(ITopicProducerProvider topicProvider, IBusControl busControl, ILogger<MotorcycleCreatedNotify> logger)
    {        
        _logger = logger;
        _busControl = busControl;        
        _topicProducer = topicProvider
            .GetProducer<MotorcycleCreatedMessage>(new Uri($"topic:{TopicName}"));        
    }

    public async Task Send(Motorcycle motorcycle)
    {
        var message = new MotorcycleCreatedMessage(motorcycle);
        try
        {
            await _busControl.StartAsync();
            await _topicProducer.Produce(message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error to notify Motorcycle created");
        }
        finally
        {
            await _busControl.StopAsync();
        }
    }
}