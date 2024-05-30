namespace API.Endpoints.Customers.SaveCustomer;

public class SaveCustomerRequest
{    
    public string Name { get; }
    public string TaxId { get; }
    public DateTime BornDate { get; }
    public int DriverLicenseNumber { get; }
    public string DriverLicenseType { get; }

    public SaveCustomerRequest(string name, string taxId, DateTime bornDate, 
        int driverLicenseNumber, string driverLicenseType)
    {
        Name = name;
        TaxId = taxId;
        BornDate = bornDate;
        DriverLicenseNumber = driverLicenseNumber;
        DriverLicenseType = driverLicenseType;
    }
}
