using Application.UseCases.Customers.SaveCustomer.Abstractions;
using Application.UseCases.Customers.SaveCustomer.Extensions;
using Application.UseCases.Customers.SaveCustomer.Ports;
using Infra.Repositories.Abstractions;

namespace Application.UseCases.Customers.SaveCustomer;

public class SaveCustomerUseCase : ISaveCustomerUseCase
{
    private readonly ICustomerRepository _repository;
    private ISaveCustomerOutputPort _outputPort = null!;

    public void SetOutputPort(ISaveCustomerOutputPort outputPort)  =>
        _outputPort = outputPort;

    public SaveCustomerUseCase(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(SaveCustomerInput input)
    {
        var customer = input.ToCustomer();

        var savedCustomers = input.IsUpdate switch
        {
            true =>  await _repository.UpdateCustomer(customer),
            false => await _repository.CreateCustomer(customer)
        };

        if (savedCustomers is null)
        {
            _outputPort.Error("Error to save Customer");
            return;
        }

        if (savedCustomers == 0 && input.IsUpdate)
        {
            _outputPort.NotFound();
            return;
        }

        var output = customer.ToOutput();

        if (input.IsUpdate)
            _outputPort.Updated(output);
        else
            _outputPort.Created(output);
    }
}
