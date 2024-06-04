using Application.UseCases.Customers.SaveCustomerDocument.Ports;

namespace Application.UseCases.Customers.SaveCustomerDocument.Abstractions;

public interface ISaveCustomerDocumentOutputPort
{
    void Ok(SaveCustomerDocumentOutput output);
    void NotFound();
    void Invalid(string message);
    void Error(string message);
}
