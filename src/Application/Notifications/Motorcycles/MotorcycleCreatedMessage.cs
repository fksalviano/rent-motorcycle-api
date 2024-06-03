using Domain.Model;

namespace Application.Notifications.Motorcycles;

public class MotorcycleCreatedMessage
{
    public Guid MotorcycleId { get; set; }
    public DateTime CreatedAt { get; set; }

    public MotorcycleCreatedMessage(Motorcycle motorcycle)
    {
        MotorcycleId = motorcycle.Id;
        CreatedAt = DateTime.Now;
    }

    public MotorcycleCreatedMessage()
    {
        // parameterless constructor to deserialize message
    }
}
