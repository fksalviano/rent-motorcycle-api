using Domain.Model;
using Infra.Repositories.Filters;

namespace Infra.Repositories.Abstractions;

public interface IMotorcycleRepository
{
    Task<IEnumerable<Motorcycle>?> GetMotorcycles(MotorcycleFilter? filter = null);
    Task<Motorcycle?> GetMotorcycle(Guid id);
    Task<int?> CreateMotorcycle(Motorcycle motorcycle);
    Task<int?> UpdateMotorcycle(Motorcycle motorcycle);
    Task<int?> DeleteMotorcycle(Guid id);
}
