using System.Diagnostics.CodeAnalysis;
using API.Endpoints.Customers.GetCustomers;
using API.Endpoints.Customers.SaveCustomer;
using API.Endpoints.Customers.SaveCustomerDocument;
using API.Endpoints.Motorcycles.GetMotorcycles;
using API.Endpoints.Motorcycles.RemoveMotorcycle;
using API.Endpoints.Motorcycles.SaveMotorcycle;
using API.Endpoints.Rents.GetRents;
using API.Endpoints.Rents.SaveRent;

namespace API.Extensions;

[ExcludeFromCodeCoverage]
public static class EndpointsInstallerExtension
{
    public static IServiceCollection  AddEndpoints(this IServiceCollection services) =>
        services
            // Motorcycles
            .AddScoped<GetMotorcyclesEndpoint>()
            .AddScoped<SaveMotorcycleEndpoint>()
            .AddScoped<RemoveMotorcycleEndpoint>()

            // Customers
            .AddScoped<GetCustomersEndpoint>()
            .AddScoped<SaveCustomerEndpoint>()
            .AddScoped<SaveCustomerDocumentEndpoint>()

            // Rents
            .AddScoped<GetRentsEndpoint>()
            .AddScoped<SaveRentEndpoint>()

            .ConfigureSerializer();
}
