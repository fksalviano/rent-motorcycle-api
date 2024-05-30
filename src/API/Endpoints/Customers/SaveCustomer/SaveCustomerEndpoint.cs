using Application.UseCases.Customers.SaveCustomer.Ports;
using Application.UseCases.Customers.SaveCustomer.Abstractions;

namespace API.Endpoints.Customers.SaveCustomer;

public class SaveCustomerEndpoint : ISaveCustomerOutputPort
{
    private readonly ISaveCustomerUseCase _useCase;
    private IResult _result = null!;

    public SaveCustomerEndpoint(ISaveCustomerUseCase useCase)
    {
        _useCase = useCase;
        _useCase.SetOutputPort(this);
    }

    public async Task<IResult> SaveCustomer(SaveCustomerRequest request)
    {
        var input = new SaveCustomerInput(request.Name, request.TaxId, request.BornDate, 
            request.DriverLicenseNumber, request.DriverLicenseType);

        await _useCase.ExecuteAsync(input);        
        return _result;
    }

    public async Task<IResult> UpdateCustomer(Guid id, SaveCustomerRequest request)
    {
        var input = new SaveCustomerInput(request.Name, request.TaxId, request.BornDate, 
            request.DriverLicenseNumber, request.DriverLicenseType, id);

        await _useCase.ExecuteAsync(input);
        return _result;
    }

    void ISaveCustomerOutputPort.Created(SaveCustomerOutput output) =>
        _result = Results.Created(string.Empty, SaveCustomerResponse.Success(output));

    void ISaveCustomerOutputPort.Updated(SaveCustomerOutput output) =>
        _result = Results.Accepted(string.Empty, SaveCustomerResponse.Success(output));


    void ISaveCustomerOutputPort.NotFound() => 
        _result = Results.NotFound(SaveCustomerResponse.Error("Id not found to update"));

    void ISaveCustomerOutputPort.Invalid(string message) =>
        _result = Results.BadRequest(SaveCustomerResponse.Error(message));

    void ISaveCustomerOutputPort.Error(string message) =>
        _result = Results.Problem(message);    
}
