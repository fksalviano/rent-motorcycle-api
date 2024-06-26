using Domain.Model.Enums;

namespace Application.UseCases.Customers.SaveCustomer.Ports;

public struct SaveCustomerInput
{
    public Guid? Id { get; }
    public string Name { get; }
    public string TaxId { get; }
    public DateTime BornDate { get; }
    public int DriverLicenseNumber { get; }
    public LicenseType DriverLicenseType { get; }

    public readonly bool IsUpdate => Id is not null;

    public SaveCustomerInput(string name, string taxId, DateTime bornDate, 
        int driverLicenseNumber, LicenseType driverLicenseType, Guid? id = null)
    {
        Id = id;
        Name = name;
        TaxId = taxId;
        BornDate = bornDate;
        DriverLicenseNumber = driverLicenseNumber;
        DriverLicenseType = driverLicenseType;
    }
}
