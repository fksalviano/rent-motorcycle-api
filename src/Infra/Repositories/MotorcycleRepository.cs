using System.Data;
using Dapper;
using Domain.Model;
using Infra.Repositories.Abstractions;
using Infra.Repositories.Filters;
using Microsoft.Extensions.Logging;

namespace Infra.Repositories;

public class MotorcycleRepository : IMotorcycleRepository
{
    private readonly IDbConnection _connection;
        private readonly ILogger<MotorcycleRepository> _logger;

    public MotorcycleRepository(IDbConnection connection, ILogger<MotorcycleRepository> logger)
    {
        _connection = connection;
        _logger = logger;
    }

    public async Task<IEnumerable<Motorcycle>?> GetMotorcycles(MotorcycleFilter? filter = null)
    {
        const string sql =
            @"select Id, Year, Model, Plate from Motorcycle
              where (Id = @Id or @Id is null)
              and (Plate = @Plate or @Plate is null)";
        try
        {
            return await _connection.QueryAsync<Motorcycle>(sql, filter);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error to get Motorcycles");
            return null;
        }
    }

    public async Task<Motorcycle?> GetMotorcycle(Guid id) =>
        (await GetMotorcycles(new MotorcycleFilter(id: id)))?.FirstOrDefault();

    public async Task<int?> CreateMotorcycle(Motorcycle motorcycle)
    {
        const string sql =
            @"insert into Motorcycle(Id, Year, Model, Plate)
            values (@Id, @Year, @Model, @Plate)";
        try
        {
            return await _connection.ExecuteAsync(sql, motorcycle);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error to save Motorcycle");
            return null;
        }
    }

    public async Task<int?> UpdateMotorcycle(Motorcycle motorcycle)
    {
        const string sql =
            @"update Motorcycle set Plate = @Plate
            where Id = @Id";
        try
        {
            return await _connection.ExecuteAsync(sql, motorcycle);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error to update Motorcycle");
            return null;
        }
    }

    public async Task<int?> DeleteMotorcycle(Guid id)
    {
        const string sql = @"delete from Motorcycle where Id = @Id";
        try
        {
            return await _connection.ExecuteAsync(sql, new { id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on delete Motorcycle");
            return null;
        }
    }
}
