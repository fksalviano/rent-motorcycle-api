namespace Application.UseCases.Customers.SaveCustomerDocument.Ports;

public struct SaveCustomerDocumentInput
{
    public Guid CustomerId { get; }
    
    public Stream? FileStream { get; }

    public string? FileType { get; set; }

    public SaveCustomerDocumentInput(Guid customerId, Stream fileStream, string fileType)
    {
        CustomerId = customerId;
        FileStream = fileStream;
        FileType = fileType;
    }

    public SaveCustomerDocumentInput(Guid customerId)
    {
        CustomerId = customerId;        
    }
}
