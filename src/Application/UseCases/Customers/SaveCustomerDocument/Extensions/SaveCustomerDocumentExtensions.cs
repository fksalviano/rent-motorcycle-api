using Application.UseCases.Customers.SaveCustomerDocument.Ports;
using Domain.Model;

namespace Application.UseCases.Customers.SaveCustomerDocument.Extensions;

public static class SaveCustomerDocumentExtensions
{
    public static SaveCustomerDocumentOutput ToOutput(this SaveCustomerDocumentInput input) =>
        new(input.CustomerId, input.FileStream!.Length);
}
