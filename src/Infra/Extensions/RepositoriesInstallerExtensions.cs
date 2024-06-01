using Infra.Repositories;
using Infra.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Extensions;

public static class RepositoriesInstallerExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services
            .AddScoped<IMotorcycleRepository, MotorcycleRepository>()
            .AddScoped<ICustomerRepository, CustomerRepository>()
            .AddScoped<IRentRepository, RentRepository>();
}
