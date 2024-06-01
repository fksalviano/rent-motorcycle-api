using Application.UseCases.Rents.GetRents.Ports;
using Application.UseCases.Rents.GetRents.Abstractions;

namespace API.Endpoints.Rents.GetRents;

public class GetRentsEndpoint : IGetRentsOutputPort
{
    private readonly IGetRentsUseCase _useCase;
    private IResult _result = null!;

    public GetRentsEndpoint(IGetRentsUseCase useCase)
    {
        _useCase = useCase;
        _useCase.SetOutputPort(this);
    }

    public async Task<IResult> GetRents(Guid? customerId)
    {
        var input = new GetRentsInput(customerId: customerId);
        
        await _useCase.ExecuteAsync(input);
        return _result;
    }

    public async Task<IResult> GetRentById(Guid id)
    {
        var input = new GetRentsInput(id);

        await _useCase.ExecuteAsync(input);
        return _result;
    }

    void IGetRentsOutputPort.Ok(GetRentsOutput output) =>
        _result = Results.Ok(GetRentsResponse.Success(output));

    void IGetRentsOutputPort.Ok(GetRentsOutputById output) =>
        _result = Results.Ok(GetRentsResponseById.Success(output));


    void IGetRentsOutputPort.NotFound() =>
        _result = Results.NotFound(GetRentsResponse.Error("Not found"));

    void IGetRentsOutputPort.Error(string message) =>
        _result = Results.Problem(message);
}
