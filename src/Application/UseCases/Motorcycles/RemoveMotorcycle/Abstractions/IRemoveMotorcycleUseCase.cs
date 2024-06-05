using Application.UseCases.Motorcycles.RemoveMotorcycle.Ports;

namespace Application.UseCases.Motorcycles.RemoveMotorcycle.Abstractions;

public interface IRemoveMotorcycleUseCase
{
    Task ExecuteAsync(RemoveMotorcycleInput input);

    void SetOutputPort(IRemoveMotorcycleOutputPort outputPort);
}
