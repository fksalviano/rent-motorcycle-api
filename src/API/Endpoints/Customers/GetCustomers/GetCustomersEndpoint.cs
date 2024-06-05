using Application.UseCases.Customers.GetCustomers.Ports;
using Application.UseCases.Customers.GetCustomers.Abstractions;

namespace API.Endpoints.Customers.GetCustomers;

public class GetCustomersEndpoint : IGetCustomersOutputPort
{
    private readonly IGetCustomersUseCase _useCase;
    private IResult _result = null!;

    public GetCustomersEndpoint(IGetCustomersUseCase useCase)
    {
        _useCase = useCase;
        _useCase.SetOutputPort(this);
    }

    public async Task<IResult> GetCustomers(string? taxId, int? driverLicenseNumber)
    {
        var input = new GetCustomersInput(taxId, driverLicenseNumber);
        
        await _useCase.ExecuteAsync(input);
        return _result;
    }

    public async Task<IResult> GetCustomerById(Guid id)
    {
        var input = new GetCustomersInput(id);

        await _useCase.ExecuteAsync(input);
        return _result;
    }

    void IGetCustomersOutputPort.Ok(GetCustomersOutput output) =>
        _result = Results.Ok(GetCustomersResponse.Success(output));

    void IGetCustomersOutputPort.Ok(GetCustomersOutputById output) =>
        _result = Results.Ok(GetCustomersResponseById.Success(output));


    void IGetCustomersOutputPort.NotFound() =>
        _result = Results.NotFound(GetCustomersResponse.Error("Not found"));

    void IGetCustomersOutputPort.Error(string message) =>
        _result = Results.Problem(message);
}
