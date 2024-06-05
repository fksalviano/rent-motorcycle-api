using Application.UseCases.Customers.SaveCustomerDocument.Ports;

namespace Application.UseCases.Customers.SaveCustomerDocument.Abstractions;

public interface ISaveCustomerDocumentUseCase
{
    Task ExecuteAsync(SaveCustomerDocumentInput input);

    void SetOutputPort(ISaveCustomerDocumentOutputPort outputPort);
}
