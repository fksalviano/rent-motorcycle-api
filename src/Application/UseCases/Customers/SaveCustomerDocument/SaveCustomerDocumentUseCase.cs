using Application.UseCases.Customers.SaveCustomerDocument.Abstractions;
using Application.UseCases.Customers.SaveCustomerDocument.Extensions;
using Application.UseCases.Customers.SaveCustomerDocument.Ports;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Customers.SaveCustomerDocument;

public class SaveCustomerDocumentUseCase : ISaveCustomerDocumentUseCase
{
    private readonly ILogger<SaveCustomerDocumentUseCase> _logger;
    private ISaveCustomerDocumentOutputPort _outputPort = null!;

    public void SetOutputPort(ISaveCustomerDocumentOutputPort outputPort) =>
        _outputPort = outputPort;

    public SaveCustomerDocumentUseCase(ILogger<SaveCustomerDocumentUseCase> logger)
    {
        _logger = logger;
    }    
    
    public async Task ExecuteAsync(SaveCustomerDocumentInput input)
    {
        var fileExtension = input.FileType!.Substring(input.FileType!.IndexOf("/") + 1);        
        var filePath = Path.Combine(Path.GetTempPath(), $"{input.CustomerId}.{fileExtension}");
        try
        {
            using (var fileStram = File.Create(filePath))

            await input.FileStream!.CopyToAsync(fileStram);            
        }
        catch (Exception ex)
        {
            _outputPort.Error("Error to save Customer Document");
            _logger.LogError(ex, $"Error to save Customer Document file {filePath}");
            return;
        }

        _logger.LogInformation($"Customer Document file saved {filePath}");

        var output = input.ToOutput();
        _outputPort.Ok(output);
    }
}
