using Domain.Model;
using Infra.Repositories.Filters;

namespace Infra.Repositories.Abstractions;

public interface IRentRepository
{
    Task<IEnumerable<Rent>?> GetRents(RentFilter? filter = null);
    Task<Rent?> GetRent(Guid id);
    Task<int?> CreateRent(Rent rent);
    Task<int?> UpdateRent(Rent rent);
    Task<int?> DeleteRent(Guid id);
}
