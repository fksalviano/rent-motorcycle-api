using Domain.Model;

namespace Application.UseCases.Motorcycles.SaveMotorcycle.Ports;

public record UpdateMotorcycleOutput(Guid Id, string Plate);
