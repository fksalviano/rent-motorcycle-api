namespace Domain.Model;

public struct Customer
{    
    public Guid Id { get; }
    public string Name { get; }
    public string TaxId { get; }
    public DateTime BornDate { get; }
    public int DriverLicenseNumber { get; }
    public string DriverLicenseType { get; }

    public Customer(Guid id, string name, string taxId, 
        DateTime bornDate, int driverLicenseNumber, string driverLicenseType)
    {
        Id = id;
        Name = name;
        TaxId = taxId;
        BornDate = bornDate;
        DriverLicenseNumber = driverLicenseNumber;
        DriverLicenseType = driverLicenseType;
    }
}

    