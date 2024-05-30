namespace Application.UseCases.Motorcycles.GetMotorcycles.Ports;

public record GetMotorcyclesInput(string? Plate = null, Guid? Id = null);    
