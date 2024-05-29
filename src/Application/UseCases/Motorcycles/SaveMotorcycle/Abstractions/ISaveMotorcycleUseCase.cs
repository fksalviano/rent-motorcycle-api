using Application.UseCases.Motorcycles.SaveMotorcycle.Ports;

namespace Application.UseCases.Motorcycles.SaveMotorcycle.Abstractions;

public interface ISaveMotorcycleUseCase
{
    Task ExecuteAsync(SaveMotorcycleInput input);

    void SetOutputPort(ISaveMotorcycleOutputPort outputPort);
}
