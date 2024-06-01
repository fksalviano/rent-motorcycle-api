using Application.UseCases.Rents.GetRents.Abstractions;
using Application.UseCases.Rents.GetRents.Extensions;
using Application.UseCases.Rents.GetRents.Ports;
using Infra.Repositories.Abstractions;

namespace Application.UseCases.Rents.GetRents;

public class GetRentsUseCase : IGetRentsUseCase
{
    private readonly IRentRepository _repository;
    private IGetRentsOutputPort _outputPort = null!;

    public void SetOutputPort(IGetRentsOutputPort outputPort)  =>
        _outputPort = outputPort;

    public GetRentsUseCase(IRentRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(GetRentsInput input)
    {
        var filter = input.ToFilter();
        var Rents = await _repository.GetRents(filter);

        if (Rents is null)
        {
            _outputPort.Error("Error to get Rents");
            return;
        }

        if (!Rents.Any())
        {
            _outputPort.NotFound();
            return;
        }

         if (input.IsGetById)
            _outputPort.Ok(Rents.First().ToOutput());
        else
            _outputPort.Ok(Rents.ToOutput());
    }

}
