using Application.UseCases.Motorcycles.SaveMotorcycleNotify.Abstractions;
using Application.UseCases.Motorcycles.SaveMotorcycleNotify.Extensions;
using Application.UseCases.Motorcycles.SaveMotorcycleNotify.Ports;
using Infra.Repositories.Abstractions;

namespace Application.UseCases.Motorcycles.SaveMotorcycleNotify;

public class SaveMotorcycleNotifyUseCase : ISaveMotorcycleNotifyUseCase
{
    private readonly IMotorcycleNotifyRepository _repository;

    private ISaveMotorcycleNotifyOutputPort _outputPort = null!;
    
    public void SetOutputPort(ISaveMotorcycleNotifyOutputPort outputPort) =>
        _outputPort = outputPort;

    public SaveMotorcycleNotifyUseCase(IMotorcycleNotifyRepository repository)
    {
        _repository = repository;
    }

    public async Task Execute(SaveMotorcycleNotifyInput input)
    {
        var motorcycleNotify = input.ToMotorcycleNotify();

        var notifySaved = await _repository.CreateMotorcycleNotify(motorcycleNotify);

        if (notifySaved is null)
        {
            _outputPort.Error("Error to save Motorcycle Notify");
            return;
        }

        var output = motorcycleNotify.ToOutput();
        _outputPort.OK(output);
    }
}
