using Domain.Model;
using Infra.Repositories.Filters;

namespace Infra.Abstractions;

public interface IMotorcycleRepository
{
    Task<IEnumerable<Motorcycle>?> GetMotorcycles(MotorcycleFilter? filter = null) ;
    Task<Motorcycle?> CreateMotorcycle(Motorcycle motorcycle);
    Task<Motorcycle?> UpdateMotorcycle(Motorcycle motorcycle);
}
