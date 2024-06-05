using Application.UseCases.Rents.SaveRent.Ports;
using Application.UseCases.Rents.SaveRent.Abstractions;

namespace API.Endpoints.Rents.SaveRent;

public class SaveRentEndpoint : ISaveRentOutputPort
{
    private readonly ISaveRentUseCase _useCase;
    private IResult _result = null!;

    public SaveRentEndpoint(ISaveRentUseCase useCase)
    {
        _useCase = useCase;
        _useCase.SetOutputPort(this);
    }

    public async Task<IResult> SaveRent(SaveRentRequest request)
    {
        var input = request.ToInput();

        await _useCase.ExecuteAsync(input);
        return _result;
    }

    public async Task<IResult> UpdateRent(Guid id, UpdateRentRequest request)
    {
        var input = request.ToInput(id);
        
        await _useCase.ExecuteAsync(input);
        return _result;
    }

    void ISaveRentOutputPort.Created(SaveRentOutput output) =>
        _result = Results.Created(string.Empty, SaveRentResponse.Success(output));

    void ISaveRentOutputPort.Updated(UpdateRentOutput output) =>
        _result = Results.Accepted(string.Empty, UpdateRentResponse.Success(output));


    void ISaveRentOutputPort.NotFound() =>
        _result = Results.NotFound(SaveRentResponse.Error("Id not found to update"));

    void ISaveRentOutputPort.Invalid(string message) =>
        _result = Results.BadRequest(SaveRentResponse.Error(message));

    void ISaveRentOutputPort.Error(string message) =>
        _result = Results.Problem(message);
}
