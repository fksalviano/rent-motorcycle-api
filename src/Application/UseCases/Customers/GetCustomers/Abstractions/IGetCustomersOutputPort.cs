using Application.UseCases.Customers.GetCustomers.Ports;

namespace Application.UseCases.Customers.GetCustomers.Abstractions;

public interface IGetCustomersOutputPort
{
    void Ok(GetCustomersOutput output);
    void Ok(GetCustomersOutputById output);    
    void NotFound();
    void Error(string message);
}
