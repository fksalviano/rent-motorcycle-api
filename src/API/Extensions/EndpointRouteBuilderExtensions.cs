using System.Diagnostics.CodeAnalysis;

namespace API.Extensions;

[ExcludeFromCodeCoverage]
public static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapGroup(this IEndpointRouteBuilder app, string tag, string prefix,
        Action<RouteGroupBuilder> mapEndpointsAction)
    {
        var group = app.MapGroup(prefix).WithTags(tag).WithOpenApi();

        mapEndpointsAction.Invoke(group);
        return app;
    }
}