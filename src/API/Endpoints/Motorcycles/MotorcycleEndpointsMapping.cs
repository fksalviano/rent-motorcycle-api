using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Domain.Base;
using API.Endpoints.Motorcycles.GetMotorcycles;
using API.Endpoints.Motorcycles.SaveMotorcycle;
using API.Endpoints.Motorcycles.RemoveMotorcycle;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace API.Endpoints.Motorcycles;

[ExcludeFromCodeCoverage]
public static class MotorcycleEndpointsMapping
{
    public static IEndpointRouteBuilder MapMotorcycleEndpoints (this IEndpointRouteBuilder app) =>
    
        app.MapGroup("Motorcycle", "/api/motorcycle", group =>
        {
            group.MapGet("/", ([FromServices] GetMotorcyclesEndpoint endpoint, [FromQuery][Optional] string? plate) => endpoint
                .GetMotorcycles(plate))
                    .WithSummary("Get Motorcycles filtering by optional params")
                    .Produces<GetMotorcyclesResponse>(Status200OK)
                    .Produces<ResponseBase<object>>(Status404NotFound);

            group.MapGet("/{id}", ([FromServices] GetMotorcyclesEndpoint endpoint, [FromRoute] Guid id) => endpoint
                .GetMotorcycleById(id))
                    .WithSummary("Get Motorcycle by Id")
                    .Produces<GetMotorcyclesResponseById>(Status200OK)
                    .Produces<ResponseBase<object>>(Status404NotFound);

            group.MapPost("/", ([FromServices] SaveMotorcycleEndpoint endpoint, [FromBody] SaveMotorcycleRequest request) => endpoint
                .SaveMotorcycle(request))
                    .WithSummary("Create a Motorcycle")
                    .Produces<SaveMotorcycleResponse>(Status201Created)
                    .Produces<ResponseBase<object>>(Status400BadRequest);

            group.MapPut("/{id}", ([FromServices] SaveMotorcycleEndpoint endpoint, [FromRoute] Guid id, [FromBody] UpdateMotorcycleRequest request) => endpoint
                .UpdateMotorcycle(id, request))
                    .WithSummary("Update a Motorcycle Plate")
                    .Produces<UpdateMotorcycleResponse>(Status202Accepted)
                    .Produces<UpdateMotorcycleResponse>(Status404NotFound)
                    .Produces<ResponseBase<object>>(Status400BadRequest);

            group.MapDelete("/{id}", ([FromServices] RemoveMotorcycleEndpoint endpoint, [FromRoute] Guid id) => endpoint
                .DeleteMotorcycle(id))
                    .WithSummary("Remove a Motorcycle if has no Rent related")
                    .Produces<UpdateMotorcycleResponse>(Status204NoContent)
                    .Produces<UpdateMotorcycleResponse>(Status404NotFound)
                    .Produces<ResponseBase<object>>(Status400BadRequest);
        });    
}
