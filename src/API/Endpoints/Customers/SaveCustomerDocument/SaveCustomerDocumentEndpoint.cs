using Application.UseCases.Customers.SaveCustomerDocument.Ports;
using Application.UseCases.Customers.SaveCustomerDocument.Abstractions;

namespace API.Endpoints.Customers.SaveCustomerDocument;

public class SaveCustomerDocumentEndpoint : ISaveCustomerDocumentOutputPort
{
    private readonly ISaveCustomerDocumentUseCase _useCase;
    private IResult _result = null!;

    public SaveCustomerDocumentEndpoint(ISaveCustomerDocumentUseCase useCase)
    {
        _useCase = useCase;
        _useCase.SetOutputPort(this);
    }

    public async Task<IResult> SaveCustomerDocument(SaveCustomerDocumentRequest? request, Guid id)
    {        
        var input = request?.ToInput(id) ?? new SaveCustomerDocumentInput(id);

        await _useCase.ExecuteAsync(input);
        return _result;
    }    

    void ISaveCustomerDocumentOutputPort.Ok(SaveCustomerDocumentOutput output) =>
        _result = Results.Accepted(string.Empty, SaveCustomerDocumentResponse.Success(output));

    void ISaveCustomerDocumentOutputPort.NotFound() =>
        _result = Results.NotFound(SaveCustomerDocumentResponse.Error("Customer Id not found"));

    void ISaveCustomerDocumentOutputPort.Invalid(string message) =>
        _result = Results.BadRequest(SaveCustomerDocumentResponse.Error(message));

    void ISaveCustomerDocumentOutputPort.Error(string message) =>
        _result = Results.Problem(message);
}
