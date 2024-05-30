using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Data;
using Npgsql;

namespace Infra.Extensions;

public static class DatabaseInstallerExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services
            .AddTransient<IDbConnection>(provider => new NpgsqlConnection(connectionString));

        return services;
    }        
}
