using Domain.Model;

namespace Application.UseCases.Motorcycles.GetMotorcycles.Ports;

public record GetMotorcyclesOutput(IEnumerable<Motorcycle> Motorcycles);
