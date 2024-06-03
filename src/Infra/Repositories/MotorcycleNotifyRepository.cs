using System.Data;
using Dapper;
using Domain.Model;
using Infra.Repositories.Abstractions;
using Microsoft.Extensions.Logging;

namespace Infra.Repositories;

public class MotorcycleNotifyRepository : IMotorcycleNotifyRepository
{

    private readonly IDbConnection _connection;
    private readonly ILogger<MotorcycleRepository> _logger;

    public MotorcycleNotifyRepository(IDbConnection connection, ILogger<MotorcycleRepository> logger)
    {
        _connection = connection;
        _logger = logger;
    }
    public async Task<int?> CreateMotorcycleNotify(MotorcycleNotify motorcycleNotify)
    {
        const string sql =
            @"insert into MotorcycleNotify (MotorcycleId, CreatedAt)
            values (@MotorcycleId, @CreatedAt)";
        try
        {
            return await _connection.ExecuteAsync(sql, motorcycleNotify);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error to save Motorcycle Notification");
            return null;
        }
    }
}
