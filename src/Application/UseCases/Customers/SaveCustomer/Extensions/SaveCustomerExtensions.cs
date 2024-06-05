using Application.UseCases.Customers.SaveCustomer.Ports;
using Domain.Model;

namespace Application.UseCases.Customers.SaveCustomer.Extensions;

public static class SaveCustomerExtensions
{
    public static SaveCustomerOutput ToOutput(this Customer Customer) =>
        new(Customer);    

    public static Customer ToCustomer(this SaveCustomerInput input) => new
    (
        input.Id ?? Guid.NewGuid(), 
        input.Name, 
        input.TaxId, 
        input.BornDate, 
        input.DriverLicenseNumber, 
        input.DriverLicenseType.ToString()
    );    
}
