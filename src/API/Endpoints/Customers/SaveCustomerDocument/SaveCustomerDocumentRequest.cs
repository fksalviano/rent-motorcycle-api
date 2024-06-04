using Application.UseCases.Customers.SaveCustomerDocument.Ports;

namespace API.Endpoints.Customers.SaveCustomerDocument;

public class SaveCustomerDocumentRequest
{
    public IFormFile File { get; }

    public SaveCustomerDocumentRequest(IFormFile file)
    {
        File = file;
    }

    public SaveCustomerDocumentInput ToInput(Guid id)
    {
        var fileStream = File.OpenReadStream();
        var fileType = File.ContentType;

        return new(id, fileStream, fileType);
    }        
}
