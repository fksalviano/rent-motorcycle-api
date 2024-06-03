using Domain.Model;

namespace Application.Notifications.Motorcycles.Abstractions;

public interface IMotorcycleCreatedNotify
{
    public Task Send(Motorcycle motorcycle);
}
