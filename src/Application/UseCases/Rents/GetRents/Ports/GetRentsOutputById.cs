using Domain.Model;

namespace Application.UseCases.Rents.GetRents.Ports;

public record GetRentsOutputById(Rent Rent, GetRentDevolutionPreview? DevolutionPreview = null);

public record GetRentDevolutionPreview(DateTime EndDate, decimal EndValue);
