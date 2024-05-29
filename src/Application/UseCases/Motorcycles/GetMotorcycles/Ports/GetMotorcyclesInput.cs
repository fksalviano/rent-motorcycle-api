namespace Application.UseCases.Motorcycles.GetMotorcycles.Ports;

public record GetMotorcyclesInput(int? Year, string? Model, string? Plate);
