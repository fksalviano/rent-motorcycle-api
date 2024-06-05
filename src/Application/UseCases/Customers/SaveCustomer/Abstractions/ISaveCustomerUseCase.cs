using Application.UseCases.Customers.SaveCustomer.Ports;

namespace Application.UseCases.Customers.SaveCustomer.Abstractions;

public interface ISaveCustomerUseCase
{
    Task ExecuteAsync(SaveCustomerInput input);

    void SetOutputPort(ISaveCustomerOutputPort outputPort);
}
