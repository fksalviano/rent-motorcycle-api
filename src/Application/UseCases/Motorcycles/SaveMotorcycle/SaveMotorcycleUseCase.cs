using Application.UseCases.Motorcycles.SaveMotorcycle.Abstractions;
using Application.UseCases.Motorcycles.SaveMotorcycle.Extensions;
using Application.UseCases.Motorcycles.SaveMotorcycle.Ports;
using Application.Notifications.Motorcycles.Abstractions;
using Infra.Repositories.Abstractions;

namespace Application.UseCases.Motorcycles.SaveMotorcycle;

public class SaveMotorcycleUseCase : ISaveMotorcycleUseCase
{
    private readonly IMotorcycleRepository _repository;
    private readonly IMotorcycleCreatedNotify _createdNotify;

    private ISaveMotorcycleOutputPort _outputPort = null!;

    public void SetOutputPort(ISaveMotorcycleOutputPort outputPort)  =>
        _outputPort = outputPort;        

    public SaveMotorcycleUseCase(IMotorcycleRepository repository, IMotorcycleCreatedNotify createdNotify)
    {            
        _repository = repository;
        _createdNotify = createdNotify;
    }

    public async Task ExecuteAsync(SaveMotorcycleInput input)
    {
        var motorcycle = input.ToMotorcycle();

        var savedMotorcycles = input.IsUpdate switch
        {
            true =>  await _repository.UpdateMotorcycle(motorcycle),
            false => await _repository.CreateMotorcycle(motorcycle)
        };        

        if (savedMotorcycles is null)
        {
            _outputPort.Error("Error to save Motorcycle");
            return;
        }

        if (savedMotorcycles == 0 && input.IsUpdate)
        {
            _outputPort.NotFound();
            return;
        }

        if (input.IsUpdate)
            _outputPort.Updated(motorcycle.ToUpdateOutput());
        else
        {            
            await _createdNotify.Send(motorcycle);
            _outputPort.Created(motorcycle.ToSaveOutput());            
        }
    }
}
