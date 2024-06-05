namespace Application.UseCases.Customers.GetCustomers.Ports;

public struct GetCustomersInput
{
    public Guid? Id { get; }    
    public string? TaxId { get; }
    public int? DriverLicenseNumber { get; }

    public readonly bool IsGetById => Id is not null;

    public GetCustomersInput(string? taxId = null, int? driverLicenseNumber = null)
    {        
        TaxId = taxId;
        DriverLicenseNumber = driverLicenseNumber;
    }

    public GetCustomersInput(Guid? id = null) => Id = id;
}
