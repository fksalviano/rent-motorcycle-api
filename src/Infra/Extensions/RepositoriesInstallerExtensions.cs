using Infra.Abstractions;
using Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Extensions;

public static class RepositoriesInstallerExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services
            .AddScoped<IMotorcycleRepository, MotorcycleRepository>();
}
