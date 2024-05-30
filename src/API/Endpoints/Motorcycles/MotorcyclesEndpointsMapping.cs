using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Domain.Base;
using API.Endpoints.Motorcycles.GetMotorcycles;
using API.Endpoints.Motorcycles.SaveMotorcycle;
using API.Endpoints.Motorcycles.RemoveMotorcycle;

namespace API.Endpoints.Motorcycles;

[ExcludeFromCodeCoverage]
public static class MotorcyclesEndpointsMapping
{
    public static void MapMotorcyclesEndpoints (this IEndpointRouteBuilder app)
    {
        app.MapGroup("Motorcycle", "/api/motorcycle", group =>
        {
            group.MapGet("/", ([FromServices] GetMotorcyclesEndpoint endpoint, [FromQuery][Optional] string? plate) => endpoint
                .GetMotorcycles(plate))
                    .WithDescription("Get Motorcycles filtering by optional params")
                    .Produces<GetMotorcyclesResponse>(StatusCodes.Status200OK)
                    .Produces<ResponseBase<object>>(StatusCodes.Status404NotFound);

            group.MapGet("/{id}", ([FromServices] GetMotorcyclesEndpoint endpoint, [FromRoute] Guid id) => endpoint
                .GetMotorcycleById(id))
                    .WithDescription("Get Motorcycle by Id")
                    .Produces<GetMotorcyclesResponse>(StatusCodes.Status200OK)
                    .Produces<ResponseBase<object>>(StatusCodes.Status404NotFound);

            group.MapPost("/", ([FromServices] SaveMotorcycleEndpoint endpoint, [FromBody] SaveMotorcycleRequest request) => endpoint
                .SaveMotorcycle(request))
                    .WithDescription("Creates a Motorcycle")
                    .Produces<SaveMotorcycleResponse>(StatusCodes.Status201Created)
                    .Produces<ResponseBase<object>>(StatusCodes.Status400BadRequest);

            group.MapPut("/{id}", ([FromServices] SaveMotorcycleEndpoint endpoint, [FromRoute] Guid id, [FromBody] UpdateMotorcycleRequest request) => endpoint
                .SaveMotorcycle(id, request))
                    .WithDescription("Update a Motorcycle")
                    .Produces<UpdateMotorcycleResponse>(StatusCodes.Status202Accepted)
                    .Produces<UpdateMotorcycleResponse>(StatusCodes.Status404NotFound)
                    .Produces<ResponseBase<object>>(StatusCodes.Status400BadRequest);

            group.MapDelete("/{id}", ([FromServices] RemoveMotorcycleEndpoint endpoint, [FromRoute] Guid id) => endpoint
                .DeleteMotorcycle(id))
                    .WithDescription("Delete a Motorcycle")
                    .Produces<UpdateMotorcycleResponse>(StatusCodes.Status204NoContent)
                    .Produces<UpdateMotorcycleResponse>(StatusCodes.Status404NotFound)
                    .Produces<ResponseBase<object>>(StatusCodes.Status400BadRequest);
        });
    }    
}
