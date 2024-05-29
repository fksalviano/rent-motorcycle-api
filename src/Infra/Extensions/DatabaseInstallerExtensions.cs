using Microsoft.Extensions.DependencyInjection;

namespace Infra.Extensions;

public static class DatabaseInstallerExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        return services;
    }        
}
