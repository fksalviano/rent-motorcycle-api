using Application.UseCases.Motorcycles.GetMotorcycles.Ports;

namespace Application.UseCases.Motorcycles.GetMotorcycles.Abstractions;

public interface IGetMotorcyclesUseCase
{
    Task ExecuteAsync(GetMotorcyclesInput input);

    void SetOutputPort(IGetMotorcyclesOutputPort outputPort);
}
