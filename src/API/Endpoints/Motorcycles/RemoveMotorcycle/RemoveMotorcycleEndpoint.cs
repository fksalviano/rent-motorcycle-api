using Application.UseCases.Motorcycles.RemoveMotorcycle.Ports;
using Application.UseCases.Motorcycles.RemoveMotorcycle.Abstractions;

namespace API.Endpoints.Motorcycles.RemoveMotorcycle;

public class RemoveMotorcycleEndpoint : IRemoveMotorcycleOutputPort
{
    private readonly IRemoveMotorcycleUseCase _useCase;
    private IResult _result = null!;

    public RemoveMotorcycleEndpoint(IRemoveMotorcycleUseCase useCase)
    {
        _useCase = useCase;
        _useCase.SetOutputPort(this);
    }

    public async Task<IResult> DeleteMotorcycle(Guid id)
    {
        var input = new RemoveMotorcycleInput(id);

        await _useCase.ExecuteAsync(input);
        return _result;
    }

    void IRemoveMotorcycleOutputPort.Ok() =>
        _result = Results.NoContent();

    void IRemoveMotorcycleOutputPort.NotFound() =>
        _result = Results.NotFound(RemoveMotorcycleResponse.Error("Id not found to delete"));

    void IRemoveMotorcycleOutputPort.Invalid(string message) =>
        _result = Results.BadRequest(RemoveMotorcycleResponse.Error(message));

    void IRemoveMotorcycleOutputPort.Error(string message) =>
        _result = Results.Problem(message);
}
