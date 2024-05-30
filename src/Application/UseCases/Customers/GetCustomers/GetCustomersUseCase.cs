using Application.UseCases.Customers.GetCustomers.Abstractions;
using Application.UseCases.Customers.GetCustomers.Extensions;
using Application.UseCases.Customers.GetCustomers.Ports;
using Infra.Repositories.Abstractions;

namespace Application.UseCases.Customers.GetCustomers;

public class GetCustomersUseCase : IGetCustomersUseCase
{
    private readonly ICustomerRepository _repository;
    private IGetCustomersOutputPort _outputPort = null!;

    public void SetOutputPort(IGetCustomersOutputPort outputPort)  =>
        _outputPort = outputPort;

    public GetCustomersUseCase(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(GetCustomersInput input)
    {
        var filter = input.ToFilter();
        var Customers = await _repository.GetCustomers(filter);

        if (Customers is null)
        {
            _outputPort.Error("Error to get Customers");
            return;
        }

        if (!Customers.Any())
        {
            _outputPort.NotFound();
            return;
        }

         if (input.IsGetById)
            _outputPort.Ok(Customers.First().ToOutput());
        else
            _outputPort.Ok(Customers.ToOutput());
    }

}
