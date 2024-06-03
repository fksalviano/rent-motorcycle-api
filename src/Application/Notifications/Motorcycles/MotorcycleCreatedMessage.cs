using Domain.Model;

namespace Application.Notifications.Motorcycles;

public class MotorcycleCreatedMessage
{
    public Motorcycle Motorcycle { get; }
    public DateTime CreatedAt { get; }

    public MotorcycleCreatedMessage(Motorcycle motorcycle, DateTime createdAt)
    {
        Motorcycle = motorcycle;
        CreatedAt = createdAt;
    }    
}
