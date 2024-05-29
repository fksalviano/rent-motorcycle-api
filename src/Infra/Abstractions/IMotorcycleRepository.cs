using Domain.Model;

namespace Infra.Abstractions;

public interface IMotorcycleRepository
{
    Task<IEnumerable<Motorcycle>?> GetMotorcycles(int? year, string? model, string? plate);
    Task<Motorcycle?> SaveMotorcycle(Motorcycle motorcycle);
}
