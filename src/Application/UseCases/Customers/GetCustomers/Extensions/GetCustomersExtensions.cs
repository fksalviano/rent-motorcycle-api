using Application.UseCases.Customers.GetCustomers.Ports;
using Domain.Model;
using Infra.Repositories.Filters;

namespace Application.UseCases.Customers.GetCustomers.Extensions;

public static class GetCustomersExtensions
{
    public static CustomerFilter ToFilter(this GetCustomersInput input) =>
        new (input.Id, input.TaxId, input.DriverLicenseNumber);

    public static GetCustomersOutput ToOutput(this IEnumerable<Customer> Customers) =>
        new(Customers);

    public static GetCustomersOutputById ToOutput(this Customer Customer) =>
        new(Customer);
}
