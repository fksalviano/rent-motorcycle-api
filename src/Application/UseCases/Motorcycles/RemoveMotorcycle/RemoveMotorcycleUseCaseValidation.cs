using Application.UseCases.Motorcycles.RemoveMotorcycle.Abstractions;
using Application.UseCases.Motorcycles.RemoveMotorcycle.Ports;
using Infra.Repositories.Abstractions;

namespace Application.UseCases.Motorcycles.RemoveMotorcycle;

public class RemoveMotorcycleUseCaseValidation : IRemoveMotorcycleUseCase
{
    private readonly IRemoveMotorcycleUseCase _useCase;
    private readonly IRentRepository _rentRepository;
    private IRemoveMotorcycleOutputPort _outputPort = null!;

    public void SetOutputPort(IRemoveMotorcycleOutputPort outputPort)
    {
         _outputPort = outputPort;
         _useCase.SetOutputPort(outputPort);
    }

    public RemoveMotorcycleUseCaseValidation(IRemoveMotorcycleUseCase useCase, IRentRepository rentRepository)
    {
        _useCase = useCase;
        _rentRepository = rentRepository;
    }

    public async Task ExecuteAsync(RemoveMotorcycleInput input)
    {        
        var rents = await _rentRepository.GetRents(new(motorcycleId: input.Id));

        if (rents is null)
        {
            _outputPort.Error("Error to get Rents on check if MotorcycleId has Rent");
            return;
        }

        if (rents.Any())
        {
            _outputPort.Invalid("MotorcycleId has Rent and can not be removed");
            return;
        }

        await _useCase.ExecuteAsync(input);
    }

}
