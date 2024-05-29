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
    
    public async Task<IResult> GetMotorcycles(GetMotorcyclesInput input)
    {
        await _useCase.ExecuteAsync(input);
        return _result;
    }

    void IGetMotorcyclesOutputPort.Ok(GetMotorcyclesOutput output) =>
        _result = Results.Ok(GetMotorcyclesResponse.Success(output));

    void IGetMotorcyclesOutputPort.NotFound() =>
        _result = Results.NotFound(GetMotorcyclesResponse.Error("Not Found"));

    void IGetMotorcyclesOutputPort.Error(string message) =>  
        _result = Results.Problem(message);
}
