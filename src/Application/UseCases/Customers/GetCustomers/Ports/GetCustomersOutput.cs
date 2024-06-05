using Domain.Model;

namespace Application.UseCases.Customers.GetCustomers.Ports;

public record GetCustomersOutput(IEnumerable<Customer> Customers);
