using Domain.Model;
using Infra.Abstractions;

namespace Infra.Repositories;

public class MotorcycleRepository : IMotorcycleRepository
{
    public async Task<IEnumerable<Motorcycle>?> GetMotorcycles(int? year, string? model, string? plate)
    {
        return new List<Motorcycle>{ new(2000, "Modelo Teste",  "Placa Teste") };
    }

    public async Task<Motorcycle?> SaveMotorcycle(Motorcycle motorcycle)
    {
        return motorcycle;
    }
}
