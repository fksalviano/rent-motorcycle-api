using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Domain.Base;
using API.Endpoints.Customers.GetCustomers;
using API.Endpoints.Customers.SaveCustomer;

namespace API.Endpoints.Customers;

[ExcludeFromCodeCoverage]
public static class CustomersEndpointsMapping
{
    public static IEndpointRouteBuilder MapCustomersEndpoints (this IEndpointRouteBuilder app) =>

        app.MapGroup("Customer", "/api/customer", group =>
        {
            group.MapGet("/", ([FromServices] GetCustomersEndpoint endpoint, [FromQuery][Optional] string? taxId, int? driverLicenseNumber) => endpoint
                .GetCustomers(taxId, driverLicenseNumber))
                    .WithDescription("Get Customers filtering by optional params")
                    .Produces<GetCustomersResponse>(StatusCodes.Status200OK)
                    .Produces<ResponseBase<object>>(StatusCodes.Status404NotFound);

            group.MapGet("/{id}", ([FromServices] GetCustomersEndpoint endpoint, [FromRoute] Guid id) => endpoint
                .GetCustomerById(id))
                    .WithDescription("Get Customer by Id")
                    .Produces<GetCustomersResponseById>(StatusCodes.Status200OK)
                    .Produces<ResponseBase<object>>(StatusCodes.Status404NotFound);

            group.MapPost("/", ([FromServices] SaveCustomerEndpoint endpoint, [FromBody] SaveCustomerRequest request) => endpoint
                .SaveCustomer(request))
                    .WithDescription("Creates a Customer")
                    .Produces<SaveCustomerResponse>(StatusCodes.Status201Created)
                    .Produces<ResponseBase<object>>(StatusCodes.Status400BadRequest);

            group.MapPut("/{id}", ([FromServices] SaveCustomerEndpoint endpoint, [FromRoute] Guid id, [FromBody] SaveCustomerRequest request) => endpoint
                .UpdateCustomer(id, request))
                    .WithDescription("Update a Customer")
                    .Produces<SaveCustomerResponse>(StatusCodes.Status202Accepted)
                    .Produces<SaveCustomerResponse>(StatusCodes.Status404NotFound)
                    .Produces<ResponseBase<object>>(StatusCodes.Status400BadRequest);
        });      
}
