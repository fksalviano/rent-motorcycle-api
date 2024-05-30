using System.Diagnostics.CodeAnalysis;
using API.Endpoints.Motorcycles;
using API.Endpoints.Customers;

namespace API.Extensions;

[ExcludeFromCodeCoverage]
public static class EndpointsMappingExtensions
{
    public static void MapEndpoints (this IEndpointRouteBuilder app) =>
        app
            .MapMotorcyclesEndpoints()
            .MapCustomersEndpoints();
}
