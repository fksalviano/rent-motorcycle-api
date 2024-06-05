using Domain.Model;

namespace Application.UseCases.Customers.SaveCustomerDocument.Ports;

public record SaveCustomerDocumentOutput(Guid CustomerId, long FileSize);
