using Application.UseCases.Motorcycles.GetMotorcycles.Ports;
using Application.UseCases.Motorcycles.GetMotorcycles.Abstractions;

namespace API.Endpoints.Motorcycles.GetMotorcycles;

public class GetMotorcyclesEndpoint : IGetMotorcyclesOutputPort
{
    private readonly IGetMotorcyclesUseCase _useCase;
    private IResult _result = null!;

    public GetMotorcyclesEndpoint(IGetMotorcyclesUseCase useCase)
    {
        _useCase = useCase;
        _useCase.SetOutputPort(this);
    }

    public async Task<IResult> GetMotorcycles(string? plate)
    {
        var input = new GetMotorcyclesInput(plate);
        
        await _useCase.ExecuteAsync(input);
        return _result;
    }

    public async Task<IResult> GetMotorcycleById(Guid id)
    {
        var input = new GetMotorcyclesInput(id);

        await _useCase.ExecuteAsync(input);
        return _result;
    }

    void IGetMotorcyclesOutputPort.Ok(GetMotorcyclesOutput output) =>
        _result = Results.Ok(GetMotorcyclesResponse.Success(output));

    void IGetMotorcyclesOutputPort.Ok(GetMotorcyclesOutputById output) =>
        _result = Results.Ok(GetMotorcyclesResponseById.Success(output));


    void IGetMotorcyclesOutputPort.NotFound() =>
        _result = Results.NotFound(GetMotorcyclesResponse.Error("Not found"));

    void IGetMotorcyclesOutputPort.Error(string message) =>
        _result = Results.Problem(message);
}
