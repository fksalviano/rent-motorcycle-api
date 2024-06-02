using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Domain.Base;
using API.Endpoints.Rents.GetRents;
using API.Endpoints.Rents.SaveRent;
using static Microsoft.AspNetCore.Http.StatusCodes;
using System.Runtime.InteropServices;

namespace API.Endpoints.Rents;

[ExcludeFromCodeCoverage]
public static class RentEndpointsMapping
{
    public static IEndpointRouteBuilder MapRentEndpoints (this IEndpointRouteBuilder app) =>
    
        app.MapGroup("Rent", "/api/rent", group =>
        {
            group.MapGet("/", ([FromServices] GetRentsEndpoint endpoint, [FromQuery] Guid? customerId) => endpoint
                .GetRents(customerId))
                    .WithSummary("Get Rents")
                    .Produces<GetRentsResponse>(Status200OK)
                    .Produces<ResponseBase<object>>(Status404NotFound);

            group.MapGet("/{id}", ([FromServices] GetRentsEndpoint endpoint, [FromRoute] Guid id, [FromQuery][Optional] DateTime? endDatePreview) => endpoint
                .GetRentById(id, endDatePreview))
                    .WithSummary("Get Rent by Id and optionally End Date to preview End Value")
                    .Produces<GetRentsResponseById>(Status200OK)
                    .Produces<ResponseBase<object>>(Status404NotFound);

            group.MapPost("/", ([FromServices] SaveRentEndpoint endpoint, [FromBody] SaveRentRequest request) => endpoint
                .SaveRent(request))
                    .WithSummary("Create a Rent for a Customer to a Motorcycle")
                    .Produces<SaveRentResponse>(Status201Created)
                    .Produces<ResponseBase<object>>(Status400BadRequest);

            group.MapPut("/{id}", ([FromServices] SaveRentEndpoint endpoint, [FromRoute] Guid id, [FromBody] UpdateRentRequest request) => endpoint
                .UpdateRent(id, request))
                    .WithSummary("Update a Rent End Date and calculate the End Value")
                    .Produces<UpdateRentResponse>(Status202Accepted)
                    .Produces<UpdateRentResponse>(Status404NotFound)
                    .Produces<ResponseBase<object>>(Status400BadRequest);
        });    
}
