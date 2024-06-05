using Application.UseCases.Customers.SaveCustomer.Ports;

namespace Application.UseCases.Customers.SaveCustomer.Abstractions;

public interface ISaveCustomerOutputPort
{
    void Created(SaveCustomerOutput output);
    void Updated(SaveCustomerOutput output);
    
    void NotFound();
    void Invalid(string message);    
    void Error(string message);
}
