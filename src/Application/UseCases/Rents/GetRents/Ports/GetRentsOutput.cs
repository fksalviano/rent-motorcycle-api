using Domain.Model;

namespace Application.UseCases.Rents.GetRents.Ports;

public record GetRentsOutput(IEnumerable<Rent> Rents);
