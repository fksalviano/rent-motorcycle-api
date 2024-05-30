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
            group.MapGet("/", ([FromServices] GetMotorcyclesEndpoint endpoint, [FromQuery] string? plate) => endpoint
                .GetMotorcycles(new GetMotorcyclesInput(plate)))

                .WithDescription("Get Motorcycles filtering by optional params")
                .Produces<GetMotorcyclesResponse>(StatusCodes.Status200OK)
                .Produces<ResponseBase<object>>(StatusCodes.Status404NotFound);

            group.MapGet("/{id}", ([FromServices] GetMotorcyclesEndpoint endpoint, [FromRoute] Guid id) => endpoint
                .GetMotorcycles(new GetMotorcyclesInput(id)))

                .WithDescription("Get Motorcycle by Id")
                .Produces<GetMotorcyclesResponse>(StatusCodes.Status200OK)
                .Produces<ResponseBase<object>>(StatusCodes.Status404NotFound);

            group.MapPost("/", ([FromServices] SaveMotorcycleEndpoint endpoint, [FromBody] SaveMotorcycleRequest request) => endpoint
                .SaveMotorcycle(new SaveMotorcycleInput(request.Year, request.Model, request.Plate)))

                .WithDescription("Creates a Motorcycle")
                .Produces<SaveMotorcycleResponse>(StatusCodes.Status201Created)
                .Produces<ResponseBase<object>>(StatusCodes.Status400BadRequest);

            group.MapPut("/{id}", ([FromServices] SaveMotorcycleEndpoint endpoint, [FromRoute] Guid id, [FromBody] UpdateMotorcycleRequest request) => endpoint
                .SaveMotorcycle(new SaveMotorcycleInput(id, request.Plate)))

                .WithDescription("Update a Motorcycle")
                .Produces<UpdateMotorcycleResponse>(StatusCodes.Status202Accepted)
                .Produces<ResponseBase<object>>(StatusCodes.Status400BadRequest);
        });
    }
}
