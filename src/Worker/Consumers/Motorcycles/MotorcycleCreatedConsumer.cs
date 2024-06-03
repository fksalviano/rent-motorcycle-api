using Microsoft.Extensions.Logging;
using MassTransit;
using Application.Notifications.Motorcycles;
using Application.UseCases.Motorcycles.SaveMotorcycleNotify.Abstractions;
using Application.UseCases.Motorcycles.SaveMotorcycleNotify.Ports;

namespace Worker.Consumers.Motorcycles;

public class MotorcycleCreatedConsumer : IConsumer<MotorcycleCreatedMessage>, ISaveMotorcycleNotifyOutputPort
{
    private readonly ISaveMotorcycleNotifyUseCase _saveNotifyUseCase;
    private readonly ILogger<MotorcycleCreatedConsumer> _logger;

    private const int YearToSaveNotify = 2024;

    public MotorcycleCreatedConsumer(ISaveMotorcycleNotifyUseCase saveNotifyUseCase, ILogger<MotorcycleCreatedConsumer> logger)
    {
        _saveNotifyUseCase = saveNotifyUseCase;
        _saveNotifyUseCase.SetOutputPort(this);

        _logger = logger;
    }

    public async Task Consume(ConsumeContext<MotorcycleCreatedMessage> context)
    {
        var message = context.Message;

        if (message.Motorcycle.Year != YearToSaveNotify)
        {
            _logger.LogInformation($"Motorcycle created notify not saved, Year is not {YearToSaveNotify} Id={message.Motorcycle.Id}");
            return;
        }

        var input = new SaveMotorcycleNotifyInput(message.Motorcycle.Id, message.CreatedAt);
        await _saveNotifyUseCase.Execute(input);
    }

    void ISaveMotorcycleNotifyOutputPort.OK(SaveMotorcycleNotifyOutput output) =>docker-compose
        _logger.LogInformation($"Motorcycle created notify saved Id={output.MotorcycleId}");

    void ISaveMotorcycleNotifyOutputPort.Error(string message) =>
        _logger.LogError(message);
}