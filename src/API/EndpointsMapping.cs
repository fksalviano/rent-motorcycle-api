using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Domain.Base;
using API.Endpoints.Motorcycles.GetMotorcycles;
using Application.UseCases.Motorcycles.GetMotorcycles.Ports;
using API.Endpoints.Motorcycles.SaveMotorcycle;
using Application.UseCases.Motorcycles.SaveMotorcycle.Ports;

[ExcludeFromCodeCoverage]
public static class EndpointsMapping
{
    public static void MapEndpoints (this IEndpointRouteBuilder app)
    {
        app.MapGroup("Motorcycle", "/api/motorcycle", group =>
        {
            group.MapGet("/", ([FromServices] GetMotorcyclesEndpoint endpoint, [FromQuery] int? year, string? model, string? plate) => endpoint
                .GetMotorcycles(new GetMotorcyclesInput(year, model, plate)))
                .WithDescription("Get Motorcycles filtering by optional params")
                .Produces<GetMotorcyclesResponse>(StatusCodes.Status200OK)
                .Produces<ResponseBase<object>>(StatusCodes.Status404NotFound);

            group.MapPost("/", ([FromServices] SaveMotorcycleEndpoint endpoint, [FromBody] SaveMotorcycleInput input) => endpoint
                .SaveMotorcycle(input))
                .WithDescription("Saves Motorcycle")
                .Produces<SaveMotorcycleResponse>(StatusCodes.Status201Created);
        });
    }
}
