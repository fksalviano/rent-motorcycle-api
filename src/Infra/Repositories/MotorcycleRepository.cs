using System.Data;
using Dapper;
using Domain.Model;
using Infra.Abstractions;
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
        const string sql = @"select Id, Year, Model, Plate from Motorcycle
                             where (Id = @Id or @Id is null)
                             and (Plate = @Plate or @Plate is null)";
        try
        {            
            return await _connection.QueryAsync<Motorcycle>(sql, filter);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error to Get Motorcycles");
            return null;
        }
    }

    public async Task<Motorcycle?> CreateMotorcycle(Motorcycle motorcycle)
    {
        const string sql = @"insert into Motorcycle(Id, Year, Model, Plate)
                             values (@Id, @Year, @Model, @Plate)";
        try
        {
            await _connection.ExecuteAsync(sql, motorcycle);
            return motorcycle;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error to Save Motorcycle");
            return null;
        }
    }

    public async Task<Motorcycle?> UpdateMotorcycle(Motorcycle motorcycle)
    {
        const string sql = @"update Motorcycle set Plate = @Plate
                             where Id = @Id";
        try
        {
            await _connection.ExecuteAsync(sql, motorcycle);
            return motorcycle;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error to Update Motorcycle");
            return null;
        }
    }
}
