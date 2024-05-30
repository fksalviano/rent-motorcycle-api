using Application.UseCases.Motorcycles.GetMotorcycles.Abstractions;
using Application.UseCases.Motorcycles.GetMotorcycles.Extensions;
using Application.UseCases.Motorcycles.GetMotorcycles.Ports;
using Infra.Repositories.Abstractions;

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
        var filter = input.ToFilter();
        var motorcycles = await _repository.GetMotorcycles(filter);

        if (motorcycles is null)
        {
            _outputPort.Error("Error to get Motorcycles");
            return;
        }

        if (!motorcycles.Any())
        {
            _outputPort.NotFound();
            return;
        }

         if (input.IsGetById)
            _outputPort.Ok(motorcycles.First().ToOutput());
        else
            _outputPort.Ok(motorcycles.ToOutput());
    }

}
