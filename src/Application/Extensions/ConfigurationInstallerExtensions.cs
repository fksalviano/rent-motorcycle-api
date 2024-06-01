using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Domain.Configuration;

namespace API.Extensions;

[ExcludeFromCodeCoverage]
public static class ConfigurationInstallerExtensions
{
    public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration) =>
        services
            .Configure<RentConfiguration>(options => configuration.GetSection("Rent").Bind(options));
}
