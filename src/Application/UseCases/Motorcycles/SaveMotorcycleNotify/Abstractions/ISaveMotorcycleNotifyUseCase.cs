using Application.UseCases.Motorcycles.SaveMotorcycleNotify.Ports;

namespace Application.UseCases.Motorcycles.SaveMotorcycleNotify.Abstractions;

public interface ISaveMotorcycleNotifyUseCase
{
    Task Execute(SaveMotorcycleNotifyInput input);

    void SetOutputPort(ISaveMotorcycleNotifyOutputPort outputPort);
}
