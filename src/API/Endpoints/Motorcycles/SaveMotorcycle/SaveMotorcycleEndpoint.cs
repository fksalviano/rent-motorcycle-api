using Application.UseCases.Motorcycles.SaveMotorcycle.Ports;
using Application.UseCases.Motorcycles.SaveMotorcycle.Abstractions;

namespace API.Endpoints.Motorcycles.SaveMotorcycle;

public class SaveMotorcycleEndpoint : ISaveMotorcycleOutputPort
{
    private readonly ISaveMotorcycleUseCase _useCase;
    private IResult _result = null!;

    public SaveMotorcycleEndpoint(ISaveMotorcycleUseCase useCase)
    {
        _useCase = useCase;
        _useCase.SetOutputPort(this);
    }

    public async Task<IResult> SaveMotorcycle(SaveMotorcycleRequest request)
    {
        var input = new SaveMotorcycleInput(request.Year, request.Model, request.Plate);

        await _useCase.ExecuteAsync(input);        
        return _result;
    }

    public async Task<IResult> SaveMotorcycle(Guid id, UpdateMotorcycleRequest request)
    {
        var input = new SaveMotorcycleInput(id, request.Plate);

        await _useCase.ExecuteAsync(input);
        return _result;
    }

    void ISaveMotorcycleOutputPort.Created(SaveMotorcycleOutput output) =>
        _result = Results.Created(string.Empty, SaveMotorcycleResponse.Success(output));

    void ISaveMotorcycleOutputPort.Updated(UpdateMotorcycleOutput output) =>
        _result = Results.Accepted(string.Empty, UpdateMotorcycleResponse.Success(output));


    void ISaveMotorcycleOutputPort.NotFound() => 
        _result = Results.NotFound(SaveMotorcycleResponse.Error("Id not found to update"));

    void ISaveMotorcycleOutputPort.Invalid(string message) =>
        _result = Results.BadRequest(SaveMotorcycleResponse.Error(message));

    void ISaveMotorcycleOutputPort.Error(string message) =>
        _result = Results.Problem(message);    
}
