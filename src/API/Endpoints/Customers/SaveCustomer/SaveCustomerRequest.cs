using Application.UseCases.Customers.SaveCustomer.Ports;
using Domain.Model.Enums;

namespace API.Endpoints.Customers.SaveCustomer;

public class SaveCustomerRequest
{    
    public string Name { get; }
    public string TaxId { get; }
    public DateTime BornDate { get; }
    public int DriverLicenseNumber { get; }
    public LicenseType DriverLicenseType { get; }

    public SaveCustomerRequest(string name, string taxId, DateTime bornDate, 
        int driverLicenseNumber, LicenseType driverLicenseType)
    {
        Name = name;
        TaxId = taxId;
        BornDate = bornDate;
        DriverLicenseNumber = driverLicenseNumber;
        DriverLicenseType = driverLicenseType;
    }

    public SaveCustomerInput ToInput(Guid? id = null) =>
        new (Name, TaxId, BornDate, DriverLicenseNumber, DriverLicenseType, id);
}
