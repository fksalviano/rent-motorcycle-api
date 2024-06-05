using Application.UseCases.Customers.GetCustomers.Ports;

namespace Application.UseCases.Customers.GetCustomers.Abstractions;

public interface IGetCustomersUseCase
{
    Task ExecuteAsync(GetCustomersInput input);

    void SetOutputPort(IGetCustomersOutputPort outputPort);
}
