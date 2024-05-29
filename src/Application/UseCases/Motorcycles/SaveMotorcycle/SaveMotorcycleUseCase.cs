using Application.UseCases.Motorcycles.SaveMotorcycle.Abstractions;
using Application.UseCases.Motorcycles.SaveMotorcycle.Extensions;
using Application.UseCases.Motorcycles.SaveMotorcycle.Ports;
using Infra.Abstractions;

namespace Application.UseCases.Motorcycles.SaveMotorcycle;

public class SaveMotorcycleUseCase : ISaveMotorcycleUseCase
{
    private readonly IMotorcycleRepository _repository;
    private ISaveMotorcycleOutputPort _outputPort = null!;

    public void SetOutputPort(ISaveMotorcycleOutputPort outputPort)  =>
        _outputPort = outputPort;

    public SaveMotorcycleUseCase(IMotorcycleRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(SaveMotorcycleInput input)
    {
        var motorcycle = input.ToMotorcycle();
        
        var motorcycleSaved = await _repository.SaveMotorcycle(motorcycle);

        if (motorcycleSaved == null)
        {
            _outputPort.Error("Error to Save Motorcycle");
            return;
        }

        var output = motorcycleSaved.ToOutput();
        _outputPort.Created(output);
    }

}
