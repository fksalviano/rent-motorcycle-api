using System.Diagnostics.CodeAnalysis;
using API.Endpoints.Motorcycles.GetMotorcycles;
using API.Endpoints.Motorcycles.SaveMotorcycle;

namespace API.Extensions;

[ExcludeFromCodeCoverage]
public static class EndpointsInstallerExtension
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services) =>
        services
            .AddScoped<GetMotorcyclesEndpoint>()
            .AddScoped<SaveMotorcycleEndpoint>();
}
