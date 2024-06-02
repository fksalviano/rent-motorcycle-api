using System.Data;
using Dapper;
using Domain.Model;
using Infra.Repositories.Abstractions;
using Infra.Repositories.Filters;
using Microsoft.Extensions.Logging;

namespace Infra.Repositories;

public class RentRepository : IRentRepository
{
    private readonly IDbConnection _connection;
    private readonly ILogger<RentRepository> _logger;

    public RentRepository(IDbConnection connection, ILogger<RentRepository> logger)
    {
        _connection = connection;
        _logger = logger;
    }

    public async Task<IEnumerable<Rent>?> GetRents(RentFilter? filter = null)
    {
        const string sql =
            @"select Id, CustomerId, MotorcycleId, RentDays, RentValue, StartDate, ExpectedEnd, EndDate, EndValue
            from Rent where (Id = @Id or @Id is null)
            and (CustomerId = @CustomerId or @CustomerId is null)
            and (MotorcycleId = @MotorcycleId or @MotorcycleId is null)";
        try
        {
            return await _connection.QueryAsync<Rent>(sql, filter);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error to get Rents");
            return null;
        }
    }

    public async Task<Rent?> GetRent(Guid id) => (await GetRents(new RentFilter(id)))?.FirstOrDefault();

    public async Task<int?> CreateRent(Rent rent)
    {
        const string sql =
            @"insert into Rent (Id, CustomerId, MotorcycleId, RentDays, RentValue, StartDate, ExpectedEnd, EndDate, EndValue)
            values(@Id, @CustomerId, @MotorcycleId, @RentDays, @RentValue, @StartDate, @ExpectedEnd, @EndDate, @EndValue)";
        try
        {
            return await _connection.ExecuteAsync(sql, rent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error to save Rent");
            return null;
        }
    }

    public async Task<int?> UpdateRent(Rent rent)
    {
        const string sql =
            @"update Rent set EndDate = @EndDate, EndValue = @EndValue
            where Id = @Id";
        try
        {
            return await _connection.ExecuteAsync(sql, rent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error to update Rent");
            return null;
        }
    }

    public async Task<int?> DeleteRent(Guid id)
    {
        const string sql = @"delete from Rent where Id = @Id";
        try
        {
            return await _connection.ExecuteAsync(sql);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error to delete Rent");
            return null;
        }
    }
}
