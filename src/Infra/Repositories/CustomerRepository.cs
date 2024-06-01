using System.Data;
using Dapper;
using Domain.Model;
using Infra.Repositories.Abstractions;
using Infra.Repositories.Filters;
using Microsoft.Extensions.Logging;

namespace Infra.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IDbConnection _connection;
        private readonly ILogger<CustomerRepository> _logger;

    public CustomerRepository(IDbConnection connection, ILogger<CustomerRepository> logger)
    {
        _connection = connection;
        _logger = logger;
    }

    public async Task<IEnumerable<Customer>?> GetCustomers(CustomerFilter? filter = null)
    {
        const string sql =
            @"select Id, Name, TaxId, BornDate, DriverLicenseNumber, DriverLicenseType 
            from Customer where (Id = @Id or @Id is null)
            and (TaxId= @TaxId or @TaxId is null)
            and (DriverLicenseNumber = @DriverLicenseNumber or @DriverLicenseNumber is null)";
        try
        {
            return await _connection.QueryAsync<Customer>(sql, filter);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error to get Customers");
            return null;
        }
    }

    public async Task<Customer?> GetCustomer(Guid id) => (await GetCustomers(new CustomerFilter(id)))?.FirstOrDefault();

    public async Task<int?> CreateCustomer(Customer customer)
    {
        const string sql =
            @"insert into Customer (Id, Name, TaxId, BornDate, DriverLicenseNumber, DriverLicenseType)
            values (@Id, @Name, @TaxId, @BornDate, @DriverLicenseNumber, @DriverLicenseType)";
        try
        {
            return await _connection.ExecuteAsync(sql, customer);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error to save Customer");
            return null;
        }
    }

    public async Task<int?> UpdateCustomer(Customer customer)
    {
        const string sql =
            @"update Customer 
            set Name = @Name, TaxId = @TaxId, BornDate = @BornDate, 
                DriverLicenseNumber = @DriverLicenseNumber, DriverLicenseType = @DriverLicenseType
            where Id = @Id";
        try
        {
            return await _connection.ExecuteAsync(sql, customer);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error to update Customer");
            return null;
        }
    }

    public async Task<int?> DeleteCustomer(Guid id)
    {
        const string sql = @"delete from Customer where Id = @Id";
        try
        {
            return await _connection.ExecuteAsync(sql, new { id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on delete Customer");
            return null;
        }
    }
}
