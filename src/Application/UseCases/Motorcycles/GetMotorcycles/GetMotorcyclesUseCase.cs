using Application.UseCases.Motorcycles.GetMotorcycles.Abstractions;
using Application.UseCases.Motorcycles.GetMotorcycles.Extensions;
using Application.UseCases.Motorcycles.GetMotorcycles.Ports;
using Infra.Abstractions;

namespace Application.UseCases.Motorcycles.GetMotorcycles;

public class GetMotorcyclesUseCase : IGetMotorcyclesUseCase
{
    private readonly IMotorcycleRepository _repository;
    private IGetMotorcyclesOutputPort _outputPort = null!;        

    public void SetOutputPort(IGetMotorcyclesOutputPort outputPort)  =>
        _outputPort = outputPort;    
    
    public GetMotorcyclesUseCase(IMotorcycleRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(GetMotorcyclesInput input)
    {        
        var motorcycles = await _repository.GetMotorcycles(input.Year, input.Model, input.Plate);

        if (motorcycles == null)
        {
            _outputPort.Error("Error to Get Motorcycles");
            return;
        }

        if (!motorcycles.Any())
        {
            _outputPort.NotFound();
            return;
        }

        var output = motorcycles.ToOutput();
        _outputPort.Ok(output);
    }    
        
}
