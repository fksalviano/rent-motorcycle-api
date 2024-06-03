namespace Infra.Repositories.Filters;

public struct CustomerFilter
{
    public Guid? Id { get; }
    public string? TaxId { get; }
    public int? DriverLicenseNumber { get; }

    public CustomerFilter(Guid? id, string? taxId, int? driverLicenseNumber)
    {
        Id = id;
        TaxId = taxId;
        DriverLicenseNumber = driverLicenseNumber;
    }

    public CustomerFilter(Guid id) => Id = id;

    public CustomerFilter(string taxId) => TaxId = taxId;
    
    public CustomerFilter(int driverLicenseNumber) => DriverLicenseNumber = driverLicenseNumber;
}
