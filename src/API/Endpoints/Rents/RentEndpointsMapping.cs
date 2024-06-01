using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Domain.Base;
using API.Endpoints.Rents.GetRents;
using API.Endpoints.Rents.SaveRent;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace API.Endpoints.Rents;

[ExcludeFromCodeCoverage]
public static class RentEndpointsMapping
{
    public static IEndpointRouteBuilder MapRentEndpoints (this IEndpointRouteBuilder app) =>
    
        app.MapGroup("Rent", "/api/rent", group =>
        {
            group.MapGet("/", ([FromServices] GetRentsEndpoint endpoint, [FromQuery] Guid? customerId) => endpoint
                .GetRents(customerId))
                    .WithDescription("Get Rents")
                    .Produces<GetRentsResponse>(Status200OK)
                    .Produces<ResponseBase<object>>(Status404NotFound);

            group.MapGet("/{id}", ([FromServices] GetRentsEndpoint endpoint, [FromRoute] Guid id) => endpoint
                .GetRentById(id))
                    .WithDescription("Get Rent by Id")
                    .Produces<GetRentsResponseById>(Status200OK)
                    .Produces<ResponseBase<object>>(Status404NotFound);

            group.MapPost("/", ([FromServices] SaveRentEndpoint endpoint, [FromBody] SaveRentRequest request) => endpoint
                .SaveRent(request))
                    .WithDescription("Creates a Rent")
                    .Produces<SaveRentResponse>(Status201Created)
                    .Produces<ResponseBase<object>>(Status400BadRequest);

            group.MapPut("/{id}", ([FromServices] SaveRentEndpoint endpoint, [FromRoute] Guid id, [FromBody] UpdateRentRequest request) => endpoint
                .UpdateRent(id, request))
                    .WithDescription("Update a Rent")
                    .Produces<UpdateRentResponse>(Status202Accepted)
                    .Produces<UpdateRentResponse>(Status404NotFound)
                    .Produces<ResponseBase<object>>(Status400BadRequest);
        });    
}
