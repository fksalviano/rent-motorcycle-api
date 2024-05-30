using Application.UseCases.Motorcycles.RemoveMotorcycle.Abstractions;
using Application.UseCases.Motorcycles.RemoveMotorcycle.Ports;
using Infra.Repositories.Abstractions;

namespace Application.UseCases.Motorcycles.RemoveMotorcycle;

public class RemoveMotorcycleUseCase : IRemoveMotorcycleUseCase
{
    private readonly IMotorcycleRepository _repository;
    private IRemoveMotorcycleOutputPort _outputPort = null!;

    public void SetOutputPort(IRemoveMotorcycleOutputPort outputPort)  =>
        _outputPort = outputPort;

    public RemoveMotorcycleUseCase(IMotorcycleRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(RemoveMotorcycleInput input)
    {        
        var deletedMotorcycles = await _repository.DeleteMotorcycle(input.Id);

        if (deletedMotorcycles is null)
        {
            _outputPort.Error("Error to delete Motorcycle");
            return;
        }

        if (deletedMotorcycles == 0)
        {
            _outputPort.NotFound();
            return;
        }
         
        _outputPort.Ok();
    }

}
