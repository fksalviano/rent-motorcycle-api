using Domain.Model;
using Infra.Repositories.Filters;

namespace Infra.Repositories.Abstractions;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>?> GetCustomers(CustomerFilter? filter = null);
    Task<Customer?> GetCustomer(Guid id);
    Task<int?> CreateCustomer(Customer customer);
    Task<int?> UpdateCustomer(Customer customer);
    Task<int?> DeleteCustomer(Guid id);
}
