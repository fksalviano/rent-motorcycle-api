using Application.UseCases.Motorcycles.RemoveMotorcycle.Abstractions;
using Application.UseCases.Motorcycles.RemoveMotorcycle.Ports;
using Infra.Repositories.Abstractions;

namespace Application.UseCases.Motorcycles.RemoveMotorcycle;

public class RemoveMotorcycleUseCaseValidation : IRemoveMotorcycleUseCase
{
    private readonly IRemoveMotorcycleUseCase _useCase;
    private readonly IMotorcycleRepository _repository;
    private IRemoveMotorcycleOutputPort _outputPort = null!;

    public void SetOutputPort(IRemoveMotorcycleOutputPort outputPort)
    {
         _outputPort = outputPort;
         _useCase.SetOutputPort(outputPort);
    }

    public RemoveMotorcycleUseCaseValidation(IRemoveMotorcycleUseCase useCase, IMotorcycleRepository repository)
    {
        _useCase = useCase;
        _repository = repository;
    }

    public async Task ExecuteAsync(RemoveMotorcycleInput input)
    {
        //TODO: change to verify if motorcycle has active rent
        
        var motorcycle = await _repository.GetMotorcycle(input.Id);

        if (motorcycle is null)
        {
            _outputPort.NotFound();
            return;
        }

        await _useCase.ExecuteAsync(input);
    }

}
