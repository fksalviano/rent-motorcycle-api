using Domain.Model;

namespace Infra.Repositories.Abstractions;

public interface IMotorcycleNotifyRepository
{    
    Task<int?> CreateMotorcycleNotify(MotorcycleNotify motorcycleNotify);    
}
